using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Settings;

public class ApplicationSettingQuery : BaseQuery<ApplicationSetting>
{
    private const string query = @"
					SELECT
						ASD.SETTING_KEY AS [Key],
						COALESCE(ASU.SETTING_VALUE, AST.SETTING_VALUE, ASD.SETTING_VALUE) AS [Value],
						ASD.DISPLAY_NAME AS Name,
						ASD.DESCRIPTION AS Description,
						ASD.DATA_TYPE AS DataType,
						COALESCE(ASU.APPLICATION_SETTINGS_ID, AST.APPLICATION_SETTINGS_ID, ASD.APPLICATION_SETTINGS_ID) AS Id,
						ASD.VALUE_LIST AS ValueList
					FROM
						APPLICATION_SETTINGS_DEFAULT ASD
                        LEFT OUTER JOIN APPLICATION_SETTINGS_TENANT AST 
                            ON AST.SETTING_KEY = ASD.SETTING_KEY
                            AND AST.TENANT_ID = @tenantId
						LEFT OUTER JOIN APPLICATION_SETTINGS_USER ASU 
                            ON ASU.SETTING_KEY = ASD.SETTING_KEY
                            AND ASU.USER_ID = @userId
                            AND ASU.TENANT_ID = @tenantId
				    WHERE
                        (ASD.APPLICATION_ID = @applicationId
                        OR ASD.APPLICATION_ID IS NULL)
                        AND ASD.SETTING_KEY LIKE 'Customization.%'";

    // Used to grab ALL application settings, not just the customizations
    private const string applicationTopmostQuery = @"
					SELECT
						ASD.SETTING_KEY AS [Key],
						COALESCE(ASU.SETTING_VALUE, AST.SETTING_VALUE, ASD.SETTING_VALUE) AS [Value],
						ASD.DISPLAY_NAME AS Name,
						ASD.DESCRIPTION AS Description,
						ASD.DATA_TYPE AS DataType,
						COALESCE(ASU.APPLICATION_SETTINGS_ID, AST.APPLICATION_SETTINGS_ID, ASD.APPLICATION_SETTINGS_ID) AS Id,
						ASD.VALUE_LIST AS ValueList
					FROM
                        APPLICATION_SETTINGS_DEFAULT ASD
                        LEFT OUTER JOIN APPLICATION_SETTINGS_TENANT AST
                            ON ASD.SETTING_KEY = AST.SETTING_KEY 
                            AND AST.TENANT_ID = @tenantId
						LEFT OUTER JOIN APPLICATION_SETTINGS_USER ASU 
                            ON ASU.SETTING_KEY = ASD.SETTING_KEY 
                            AND ASU.USER_ID = @userId 
                            AND ASU.TENANT_ID = @tenantId
				    WHERE
                        ASD.APPLICATION_ID = @applicationId";



    public override QueryInfo GetQuery(Criteria criteria)
    {
        var formattedQuery = "";
        if (criteria.Parameters.ContainsKey("findMode") && criteria.Parameters["findMode"]?.ToString() == "topmost") 
        {
            formattedQuery = FilterAndSearchQuery(applicationTopmostQuery, criteria);
        }
        else
        {
            formattedQuery = FilterAndSearchQuery(query, criteria);
        }

        return new QueryInfo
        {
            Query = formattedQuery,
            Parameters = new DynamicParameters(criteria.Parameters)
        };
    }
}

