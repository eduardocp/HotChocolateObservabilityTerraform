# resource "azurerm_resource_group" "rg" {
#   name     = "dotnet-hotchocolate-rg"
#   location = "West Europe"
# }

# resource "azurerm_service_plan" "asp" {
#   name                = "dotnet-hotchocolate-asp"
#   location            = azurerm_resource_group.rg.location
#   resource_group_name = azurerm_resource_group.rg.name
#   os_type             = "Linux"
#   sku_name            = "F1"
# }

# resource "azurerm_linux_web_app" "app" {
#   name                = "dotnet-hotchocolate-app"
#   location            = azurerm_resource_group.rg.location
#   resource_group_name = azurerm_resource_group.rg.name
#   service_plan_id     = azurerm_service_plan.asp.id

#   site_config {
#     always_on = false # Set to false because free tiers can't use always on
#     application_stack {
#         dotnet_version = "8.0"
#     }
#   }

#   app_settings = {
#     "WEBSITE_RUN_FROM_PACKAGE"               = "1"
#     "APPINSIGHTS_INSTRUMENTATIONKEY"         = azurerm_application_insights.appinsights.instrumentation_key
#     "APPLICATIONINSIGHTS_CONNECTION_STRING"  = azurerm_application_insights.appinsights.connection_string
#   }
# }

# resource "azurerm_application_insights" "appinsights" {
#   name                = "dotnet-hotchocolate-appinsights"
#   location            = azurerm_resource_group.rg.location
#   resource_group_name = azurerm_resource_group.rg.name
#   application_type    = "web"
# }