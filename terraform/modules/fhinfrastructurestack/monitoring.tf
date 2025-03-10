locals {
  # The template is designed in the development environment so we need to replace the prefix and subscription
  # with appropriate environment values
  dashboard_json1 = file("files/dashboard/template.json")
  dashboard_json2 = replace(local.dashboard_json1, "s181d01", var.prefix)
  dashboard_json = replace(local.dashboard_json2, "56c128b8-5ca9-4328-bb24-a11c829f4af9", var.subscription_id)
}

resource "azurerm_log_analytics_workspace" "app_services" {
  name = "${var.prefix}-la-as-familyhubs"
  resource_group_name = local.resource_group_name
  location = var.location
  retention_in_days = var.log_retention_in_days
  tags = local.tags
}

resource "azurerm_application_insights" "app_insights" {
  name = "${var.prefix}-ai-as-familyhubs"
  resource_group_name = local.resource_group_name
  location = var.location
  application_type = "web"
  sampling_percentage = 0
  workspace_id = azurerm_log_analytics_workspace.app_services.id
  retention_in_days = var.log_retention_in_days
  tags = local.tags
}

resource "azurerm_application_insights_workbook" "dashboard" {
  name = "c92bc228-b32d-4765-b4a1-dab52508ee31"
  resource_group_name = local.resource_group_name
  location = var.location
  display_name = "Family Hubs Dashboard"
  data_json = jsonencode(local.dashboard_json)
  tags = local.tags
}