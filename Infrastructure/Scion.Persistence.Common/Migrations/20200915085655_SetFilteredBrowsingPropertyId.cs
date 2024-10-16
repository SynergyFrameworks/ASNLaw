using Microsoft.EntityFrameworkCore.Migrations;

namespace Scion.Data.Migrations
{
    public partial class SetFilteredBrowsingPropertyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PlatformDynamicProperty'))
                BEGIN
                    DELETE FROM PlatformDynamicPropertyName WHERE PropertyId IN
                    (
                        SELECT Id FROM [PlatformDynamicProperty]
                         WHERE Name = 'FilteredBrowsing' AND ObjectType IN ('Scion.Domain.Store.Model.Store','Scion.StoreModule.Core.Model.Store')
                    )

                    DELETE FROM PlatformDynamicPropertyObjectValue WHERE PropertyId IN
                    (
                        SELECT Id FROM [PlatformDynamicProperty]
                         WHERE Name = 'FilteredBrowsing' AND ObjectType IN ('Scion.Domain.Store.Model.Store','Scion.StoreModule.Core.Model.Store')
                    )

                    UPDATE [PlatformDynamicProperty]
                       SET Id = 'Scion.Catalog_FilteredBrowsing_Property'
                     WHERE Name = 'FilteredBrowsing' AND ObjectType IN ('Scion.Domain.Store.Model.Store','Scion.StoreModule.Core.Model.Store') AND Id != 'Scion.Catalog_FilteredBrowsing_Property'
                END");

            migrationBuilder.Sql(@"IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StoreDynamicPropertyObjectValue'))
                BEGIN
                    UPDATE [StoreDynamicPropertyObjectValue]
                       SET PropertyId = 'Scion.Catalog_FilteredBrowsing_Property'
                     WHERE PropertyName = 'FilteredBrowsing' AND ObjectType = 'Scion.StoreModule.Core.Model.Store'	
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Method intentionally left empty.
        }
    }
}
