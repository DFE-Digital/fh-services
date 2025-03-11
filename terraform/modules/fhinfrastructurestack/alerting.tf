####################################################################################################
#
# Data sources
#
####################################################################################################

# Azure data factory is managed outside of terraform and is an OPTIONAL environment component in
# test environments

data "azurerm_data_factory" "adf_dataf_default" {
  count = var.data_factory_exists ? 1 : 0
  name = "${var.prefix}-dataf-default"
  resource_group_name = local.resource_group_name
}

####################################################################################################
#
# Locals
#
####################################################################################################

locals {
  # All production alerts go into a separate silver monitor resource group otherwise add to the same resource group
  alert_resource_group_name = var.environment == "Prod" ? "${var.prefix}-silverMonitor" : local.resource_group_name
  
  gateway_details = {
    "referral-ui" = {
      gateway_id = azurerm_application_gateway.ref_ui_app_gateway.id
    },
    "sd-admin-ui" = {
      gateway_id = azurerm_application_gateway.sd_admin_ui_app_gateway.id
    },
    "sd-ui" = {
      gateway_id = azurerm_application_gateway.sd_ui_app_gateway.id
    }
  }

  service_plan_details = {
    "primary" = {
      service_plan_id = azurerm_service_plan.apps_plan.id
    }
  }

  app_service_details = {
    "fh_referral_api" = {
      app_service_id = azurerm_windows_web_app.fh_referral_api.id
    },
    "fh_notification_api" = {
      app_service_id = azurerm_windows_web_app.fh_notification_api.id
    },
    "fh_idam_api" = {
      app_service_id = azurerm_windows_web_app.fh_idam_api.id
    },
    "fh_referral_ui" = {
      app_service_id = azurerm_windows_web_app.fh_referral_ui.id
    },
    "fh_report_api" = {
      app_service_id = azurerm_windows_web_app.fh_report_api.id
    },
    "fh_sd_api" = {
      app_service_id = azurerm_windows_web_app.fh_sd_api.id
    },
    "fh_sd_admin_ui" = {
      app_service_id = azurerm_windows_web_app.fh_sd_admin_ui.id
    },
    "fh_sd_ui" = {
      app_service_id = azurerm_windows_web_app.fh_sd_ui.id
    },
  }

  adf_details = var.data_factory_exists ? {
    "fh_adf" = {
      adf_id = data.azurerm_data_factory.adf_dataf_default[0].id
    }
  } : {}
}

####################################################################################################
#
# Alert action group
#
####################################################################################################

resource "azurerm_monitor_action_group" "slack_channel_email_action_group" {
  name = "${var.prefix}-fh-ag-slack-channel-email"
  resource_group_name = local.alert_resource_group_name
  short_name = "fhalert"
  email_receiver {
    email_address = var.slack_support_channel_email
    name = "FamilyHub"
  }
  tags = local.tags
}

####################################################################################################
#
# App insight alerts
#
####################################################################################################

resource "azurerm_monitor_metric_alert" "cpu_alert01" {
  name                  = "${var.prefix}-fh-cpu-alert-referral-ui"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "CPU-Utilization is greater than 75%"
  window_size           = "PT30M"
  frequency             = "PT5M"
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/processCpuPercentage"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 75
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "cpu_alert02" {
  name                  = "${var.prefix}-fh-cpu-alert-sd-api"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "CPU-Utilization is greater than 75%"
  window_size           = "PT15M"
  frequency             = "PT5M"  
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/processCpuPercentage"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 75
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "cpu_alert03" {
  name                  = "${var.prefix}-fh-cpu-alert-sd-ui"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "CPU-Utilization is greater than 75%"
  window_size           = "PT15M"
  frequency             = "PT5M"  
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/processCpuPercentage"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 75
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "cpu_alert04" {
  name                  = "${var.prefix}-fh-cpu-alert-sd-admin-ui"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "CPU-Utilization is greater than 75%"
  window_size           = "PT15M"
  frequency             = "PT5M"  
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/processCpuPercentage"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 75
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "response-01" {
  name                  = "${var.prefix}-fh-response-alert-referral-ui"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "Response Time alert has reached the threshold"
  window_size           = "PT15M"
  frequency             = "PT5M"  
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/requestExecutionTime"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 10000
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "response-02" {
  name                  = "${var.prefix}-fh-response-alert-sd-api"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "Response Time alert has reached the threshold"
  window_size           = "PT15M"
  frequency             = "PT5M"  
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/requestExecutionTime"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 10000
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "response-03" {
  name                  = "${var.prefix}-fh-response-alert-sd-ui"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "Response Time alert has reached the threshold"
  window_size           = "PT15M"
  frequency             = "PT5M"  
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/requestExecutionTime"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 10000
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "response-04" {
  name                  = "${var.prefix}-fh-response-alert-sd-admin-ui"
  resource_group_name   = local.alert_resource_group_name
  scopes                = [azurerm_application_insights.app_insights.id]
  description           = "Response Time alert has reached the threshold"
  window_size           = "PT15M"
  frequency             = "PT5M"  
  action {
    action_group_id     =  azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation         = "Average"
    metric_name         = "performanceCounters/requestExecutionTime"
    metric_namespace    = "Microsoft.Insights/components"
    operator            = "GreaterThan"
    threshold           = 10000
  }
  tags = local.tags
}

####################################################################################################
#
# App gateway alerts
#
####################################################################################################

resource "azurerm_monitor_metric_alert" "app-gateway-total-time-alert" {
  for_each = local.gateway_details
  name = "${var.prefix}-fh-appgw-${each.key}-total-time-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.gateway_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  dynamic_criteria {
    aggregation = "Average"
    alert_sensitivity = "Medium"
    metric_name = "ApplicationGatewayTotalTime"
    metric_namespace = "Microsoft.Network/applicationGateways"
    operator = "GreaterThan"
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-gateway-backend-connect-time-alert" {
  for_each = local.gateway_details
  name = "${var.prefix}-fh-appgw-${each.key}-backend-connect-time-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.gateway_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "BackendConnectTime"
    metric_namespace = "Microsoft.Network/applicationGateways"
    operator = "GreaterThan"
    threshold = 30
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-gateway-backend-last-byte-response-time-alert" {
  for_each = local.gateway_details
  name = "${var.prefix}-fh-appgw-${each.key}-backend-last-byte-response-time-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.gateway_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  dynamic_criteria {
    aggregation = "Average"
    alert_sensitivity = "Medium"
    metric_name = "BackendLastByteResponseTime"
    metric_namespace = "Microsoft.Network/applicationGateways"
    operator = "GreaterThan"
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-gateway-backend-response-status-alert" {
  for_each = local.gateway_details
  name = "${var.prefix}-fh-appgw-${each.key}-backend-response-status-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.gateway_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  dynamic_criteria {
    aggregation = "Total"
    alert_sensitivity = "Medium"
    metric_name = "BackendResponseStatus"
    metric_namespace = "Microsoft.Network/applicationGateways"
    operator = "GreaterThan"
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-gateway-failed-requests-alert" {
  for_each = local.gateway_details
  name = "${var.prefix}-fh-appgw-${each.key}-failed-requests-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.gateway_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  dynamic_criteria {
    aggregation = "Total"
    alert_sensitivity = "Medium"
    metric_name = "FailedRequests"
    metric_namespace = "Microsoft.Network/applicationGateways"
    operator = "GreaterThan"
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-gateway-unhealthy-host-count-alert" {
  for_each = local.gateway_details
  name = "${var.prefix}-fh-appgw-${each.key}-unhealthy-host-count-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.gateway_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "UnhealthyHostCount"
    metric_namespace = "Microsoft.Network/applicationGateways"
    operator = "GreaterThan"
    threshold = 0
  }
  tags = local.tags
}

# Availability of storage account which hosts the custom error pages

resource "azurerm_monitor_metric_alert" "app-gateway-storage-error-availability-alert" {
  name = "${var.prefix}-fh-appgw-saappgwerror-availability-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [azurerm_storage_account.storage_appgw_errorpage.id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "Availability"
    metric_namespace = "Microsoft.Storage/storageAccounts"
    operator = "LessThan"
    threshold = 90
  }
  tags = local.tags
}

####################################################################################################
#
# App service plan alerts
#
####################################################################################################

resource "azurerm_monitor_metric_alert" "app-service-plan-cpu-percentage-alert" {
  for_each = local.service_plan_details
  name = "${var.prefix}-fh-srvplan-${each.key}-cpu-percentage-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.service_plan_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "CpuPercentage"
    metric_namespace = "Microsoft.Web/serverfarms"
    operator = "GreaterThan"
    threshold = 80
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-service-plan-http-queue-length-alert" {
  for_each = local.service_plan_details
  name = "${var.prefix}-fh-srvplan-${each.key}-http-queue-length-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.service_plan_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "HttpQueueLength"
    metric_namespace = "Microsoft.Web/serverfarms"
    operator = "GreaterThan"
    threshold = 20
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-service-plan-memory-percentage-alert" {
  for_each = local.service_plan_details
  name = "${var.prefix}-fh-srvplan-${each.key}-memory-percentage-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.service_plan_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "MemoryPercentage"
    metric_namespace = "Microsoft.Web/serverfarms"
    operator = "GreaterThan"
    threshold = 80
  }
  tags = local.tags
}

####################################################################################################
#
# App service alerts
#
####################################################################################################

resource "azurerm_monitor_metric_alert" "app-service-server-errors-alert" {
  for_each = local.app_service_details
  name = "${var.prefix}-fh-appplan-${each.key}-server-errors-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.app_service_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "Http5xx"
    metric_namespace = "Microsoft.Web/sites"
    operator = "GreaterThan"
    threshold = 0
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-service-cpu-time-alert" {
  # Function apps do not support the CpuTime metric, so don't create this alert for function apps
  for_each = { 
    for key, app in local.app_service_details :
    key => app if !lookup(app, "is_function_app", false)
  }  
  name = "${var.prefix}-fh-appplan-${each.key}-cpu-time-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.app_service_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  dynamic_criteria {
    aggregation = "Total"
    alert_sensitivity = "Medium"
    metric_name = "CpuTime"
    metric_namespace = "Microsoft.Web/sites"
    operator = "GreaterThan"
  }
  tags = local.tags
}

resource "azurerm_monitor_metric_alert" "app-service-http-response-time-alert" {
  for_each = local.app_service_details
  name = "${var.prefix}-fh-appplan-${each.key}-http-response-time-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.app_service_id]
  window_size = "PT15M"
  frequency = "PT5M"
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Average"
    metric_name = "HttpResponseTime"
    metric_namespace = "Microsoft.Web/sites"
    operator = "GreaterThan"
    threshold = 10
  }
  tags = local.tags
}

####################################################################################################
#
# Azure Data Factory alerts
#
####################################################################################################

resource "azurerm_monitor_metric_alert" "adf-failed-pipeline-runs-alert" {
  for_each = local.adf_details
  name = "${var.prefix}-fh-adf-${each.key}-failed-pipeline-runs-alert"
  resource_group_name = local.alert_resource_group_name
  scopes = [each.value.adf_id]
  window_size = "PT30M"
  frequency = "PT1M"
  severity = 1
  action {
    action_group_id = azurerm_monitor_action_group.slack_channel_email_action_group.id
  }
  criteria {
    aggregation = "Total"
    metric_name = "PipelineFailedRuns"
    metric_namespace = "Microsoft.DataFactory/factories"
    operator = "GreaterThan"
    threshold = 0
  }
  tags = local.tags
}
