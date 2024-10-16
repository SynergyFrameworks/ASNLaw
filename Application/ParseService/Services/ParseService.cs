using AutoMapper;
using Datalayer.Enum;
using DocumentFormat.OpenXml.Packaging;
using Domain.Parse.Contracts;
using Domain.Parse.Model;
using Infrastructure;
using Infrastructure.Common.Commands;
using Infrastructure.Common.Queries;
using Microsoft.AspNetCore.Http;
using ParseService.Commands;
using ParseService.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace ParseService.Services
{
    public class ParseService
    {

        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;
        private readonly IParseParameterService _parameterService;
        private readonly IParserMongoService _parserMongoService;
        private readonly IParser _parser;

        public ParseService(IMapper mapper,
                               IParseParameterService parameterService,
                               IParserMongoService parserMongoService,
                               IParser parser,
                               IQueryBus queryBus,
                               ICommandBus commandBus
                               )
        {


            _mapper = mapper;
            _parameterService = parameterService;
            _parserMongoService = parserMongoService;
            _parser = parser;
            _queryBus = queryBus;
            _commandBus = commandBus;


        }

        const String folderName = "files";
        readonly String folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        // Assuming AutoMapper is set up to map between Datalayer.Domain.Parameter and Domain.Parse.Model.ParseParameter
        public async Task<ParseResult> CreateLegalParse(ParseArgs parseArgs, Progress<double> pi, CancellationToken cancellationToken)
        {
            var contentExtraction = ExtractStylesPart(parseArgs.File.FileName, true);
            parseArgs.txtContent = contentExtraction.ToString();

            IList<Datalayer.Domain.Parameter> presult = await _parameterService.GetAll(ParameterType.Parse);

            // Map Datalayer.Domain.Parameter to Domain.Parse.Model.ParseParameter
            IList<ParseParameter> mappedParameters = _mapper.Map<IList<ParseParameter>>(presult);

            // Assign the mapped parameters to parseArgs.Parameters
            parseArgs.Parameters = mappedParameters;

            var result = await _parser.ParseLegal(parseArgs, pi, cancellationToken);

            try
            {
                _parserMongoService.CreateDocument(result);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Parse Legal " + e + " Exception");
            }

            CreateParseCommand.Command command = Mapping.Map<ParseResult, CreateParseCommand.Command>(result);
            CreateParseCommand.Result resultMB = await _commandBus.Send(command, cancellationToken);

            return result;
        }


        public async Task<ParseResult> CreateParagraphParse(ParseArgs parseArgs, Progress<double> pi, CancellationToken cancellationToken)
        {

            //    string tempFilePath = await WriteTempFileAsync(folderPath, Form.File);

            //    Application application = new();

            //    string docName = tempFilePath;
            //    Document document = application.Documents.Open(
            //        docName,
            //        NoEncodingDialog: true

            //       );
            //    string text = document.Content.Text;

            //    application.Quit();



            var result = await _parser.ParseParagraph(parseArgs, pi, cancellationToken).ConfigureAwait(false);

            try
            {
                _parserMongoService.CreateDocument(result);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Parse Paragraph " + e + " Exception");
            }

            CreateParseCommand.Command command = Mapping.Map<ParseResult, CreateParseCommand.Command>(result);
            CreateParseCommand.Result resultMB = await _commandBus.Send(command, cancellationToken);


            return result;


        }



        private async Task<string> WriteTempFileAsync(string folderPath, IFormFile sourceFile)
        {
            string path = System.IO.Path.Combine(folderPath, sourceFile.FileName);
            using (MemoryStream fileContentStream = new MemoryStream())
            {
                await sourceFile.CopyToAsync(fileContentStream);
                await System.IO.File.WriteAllBytesAsync(path, fileContentStream.ToArray());
            }

            return path;
        }


        // Extract the styles or stylesWithEffects part from a 
        // word processing document as an XDocument instance.
        public static XDocument ExtractStylesPart(
          string fileName,
          bool getStylesWithEffectsPart = true)
        {
            // Declare a variable to hold the XDocument.
            XDocument styles = null;

            // Open the document for read access and get a reference.
            using (var document =
                WordprocessingDocument.Open(fileName, false))
            {
                // Get a reference to the main document part.
                var docPart = document.MainDocumentPart;

                // Assign a reference to the appropriate part to the
                // stylesPart variable.
                StylesPart stylesPart = null;
                if (getStylesWithEffectsPart)
                    stylesPart = docPart.StylesWithEffectsPart;
                else
                    stylesPart = docPart.StyleDefinitionsPart;

                // If the part exists, read it into the XDocument.
                if (stylesPart != null)
                {
                    using (var reader = XmlNodeReader.Create(
                      stylesPart.GetStream(FileMode.Open, FileAccess.Read)))
                    {
                        // Create the XDocument.
                        styles = XDocument.Load(reader);
                    }
                }
            }
            // Return the XDocument instance.
            return styles;
        }


    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Datalayer.Domain.Parameter, Domain.Parse.Model.ParseParameter>();
            // Add other mappings as needed
        }
    }


}
