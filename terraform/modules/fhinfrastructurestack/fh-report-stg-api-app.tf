# Report Staging API Application Group

# Create Application Insights for Report Staging API 
# resource "azurerm_application_insights" "fh_report_stg_api_app_insights" {
#   name                  = "${var.prefix}-as-fh-report-stg-api"
#   resource_group_name   = "${local.resource_group_name}"
#   location              = "${var.location}"
#   application_type      = "web"
#   sampling_percentage   = 0
#   workspace_id          = azurerm_log_analytics_workspace.app_services.id
#          tags = {
#           "Parent Business"     = local.parent_business
#           "Service Offering"    = local.Service_Offering
#           "Portfolio"           = local.Portfolio
#           "Service Line"        = local.Service_Line
#           "Service"             = local.Service
#           "Product"             = local.Product
#           "Environment"         = var.environment
#   }
# }

# Create App Service for Report Staging API
# resource "azurerm_windows_web_app" "fh_report_stg_api" {
#   app_settings = {
#     APPINSIGHTS_INSTRUMENTATIONKEY              = "${azurerm_application_insights.fh_report_stg_api_app_insights.instrumentation_key}"
#     ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
#     XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
#     ASPNETCORE_ENVIRONMENT                      = "${var.asp_netcore_environment}"
#     WEBSITE_RUN_FROM_PACKAGE                    = "1"
#   }
#   name                                          = "${var.prefix}-as-fh-report-stg-api"
#   resource_group_name                           = "${local.resource_group_name}"
#   location                                      = "${var.location}"
#   service_plan_id                               = azurerm_service_plan.apps_plan.id
#   client_affinity_enabled                       = false
#   https_only                                    = true
#   identity {
#     type                                        = "SystemAssigned"
#   }
#     site_config {
#     always_on                                   = true
#     ftps_state                                  = "Disabled"
#     health_check_path                           = "/api/health"
#     application_stack {
#     current_stack                               = "${var.current_stack}"
#     dotnet_version                              = "${var.dotnet_version_general}"
#   }
#   ip_restriction {
#       name                                      = "ADO Access"
#       action                                    = "Allow"
#       priority                                  = "1"
#       service_tag                               = "AzureCloud"
#   }
#   ip_restriction {
#       name                                      = "Swagger.io Access"
#       action                                    = "Allow"
#       priority                                  = "2"
#       ip_address                                = "3.134.212.167/32"
#   }
#   ip_restriction {
#       name                                      = "Harsha Reddy Local Machine"
#       action                                    = "Allow"
#       priority                                  = "4"
#       ip_address                                = "31.53.249.6/32"
#   }
#   scm_ip_restriction {
#       name                                      = "ADO Access"
#       action                                    = "Allow"
#       priority                                  = "1"
#       service_tag                               = "AzureCloud"
#   }
#   scm_ip_restriction {
#       name                                      = "Swagger.io Access"
#       action                                    = "Allow"
#       priority                                  = "2"
#       ip_address                                = "3.134.212.167/32"
#   }
#   scm_ip_restriction {
#       name                                      = "Harsha Reddy Local Machine"
#       action                                    = "Allow"
#       priority                                  = "4"
#       ip_address                                = "31.53.249.6/32"
#   }
#   }
#   logs {
#   application_logs {
#     file_system_level                           = "Error"
#     azure_blob_storage {
#       level                                     = "Error"
#       sas_url                                   = "${var.sas_report_stg_api_app_logging}"
#       retention_in_days                         = "28"
#     }
#   }
#   http_logs {
#     azure_blob_storage {
#       sas_url                                   = "${var.sas_report_stg_api_web_logging}"
#       retention_in_days                         = "28"
#     } 
#   }
#   }
#   tags = var.resource_tags
# }

## Swift Connection for Report Staging API
#resource "azurerm_app_service_virtual_network_swift_connection" "fh_report_stg_api" {
#  app_service_id = azurerm_windows_web_app.fh_report_stg_api.id
#  subnet_id      = azurerm_subnet.vnetint.id
#}

# SQL Server Instance
resource "azurerm_mssql_server" "reporting_sql_server" {
  name = "${var.prefix}-as-fh-sql-server-reporting"
  resource_group_name = local.resource_group_name
  location = var.location
  version = "12.0"

  administrator_login = var.sql_server_reporting_user
  administrator_login_password = var.sql_server_reporting_pwd

  tags = local.tags
}

# SQL Server Database for Report Staging API
resource "azurerm_mssql_database" "report_staging_db" {
  name = "${var.prefix}-fh-report-staging-db"
  server_id = azurerm_mssql_server.reporting_sql_server.id
  collation = "SQL_Latin1_General_CP1_CI_AS"

  max_size_gb = 250
  sku_name = var.environment == "Dev" ? "S2" : "S3"
  zone_redundant = false
  storage_account_type = "Local"

  short_term_retention_policy {
      retention_days = "7"
      backup_interval_in_hours = "24"
  }

  long_term_retention_policy {
      monthly_retention = "PT0S"
  }

  threat_detection_policy {
      disabled_alerts = []
      email_account_admins = "Disabled"
      email_addresses = []
      retention_days = 0
      state = "Disabled"
  }

  tags = local.tags
}

# Private Endpoint 12: Report Staging API
# resource "azurerm_private_endpoint" "reportstgapi" {
#   name                = "${var.prefix}-as-fh-report-stg-api"
#   location            = "${var.location}"
#   resource_group_name = "${local.resource_group_name}"
#   subnet_id           = azurerm_subnet.pvtendpoint.id
#   custom_network_interface_name = "${var.prefix}-as-fh-report-stg-api-nic"

#   ip_configuration {
#     name                       = "${var.prefix}-as-fh-report-stg-api"
#     private_ip_address         = "${var.pvt_endp_report_stg_api_ip_address}"
#     subresource_name           = "sites" 
#   }

#  private_service_connection {
#     name                           = "${var.prefix}-pvtendpt-report-stg-api"
#     private_connection_resource_id = azurerm_windows_web_app.fh_report_stg_api.id
#     is_manual_connection           = false
#     subresource_names              = [ "sites" ]
#   }

#   private_dns_zone_group {
#     name                 = azurerm_private_dns_zone.appservices.name
#     private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
#   }
#   tags = var.resource_tags
# }

# Private DNS Zone - SQL Server A Records
resource "azurerm_private_dns_a_record" "sql_report_stg_api" {
  name                = "${var.prefix}-as-fh-report-stg-api"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.pvt_endp_report_stg_api_ip_address]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_report_stg_api_scm" {
  name                = "${var.prefix}-as-fh-report-stg-api.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.pvt_endp_report_stg_api_ip_address]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}


# Storage Accounts for Report Staging API Logging
resource "azurerm_storage_account" "storage_rep_stg_api_logs" {
  name                = "${var.prefix}sarepstgapilogs"
  resource_group_name = local.resource_group_name
  location            = var.location
  account_tier             = local.account_tier
  account_kind             = local.account_kind
  access_tier              = local.access_tier
  min_tls_version          = local.min_tls_version
  is_hns_enabled           = local.is_hns_enabled
  public_network_access_enabled = local.public_network_access_enabled_storage
  account_replication_type = local.account_replication_type
  infrastructure_encryption_enabled = local.infrastructure_encryption_enabled
  blob_properties {
    versioning_enabled     = local.versioning_enabled
    change_feed_enabled    = local.change_feed_enabled
    delete_retention_policy {
      days                 = local.delete_retention_policy_days
    }
    container_delete_retention_policy {
      days                 = local.container_delete_retention_policy_days
    }
  }
  tags = local.tags
}

resource "azurerm_storage_container" "container_rep_stg_api_app_logs" {
  name                  = "${var.prefix}sarepstgapilogsapplogs"
  storage_account_name  = azurerm_storage_account.storage_rep_stg_api_logs.name
  container_access_type = "blob"
}

resource "azurerm_storage_container" "container_rep_stg_api_web_logs" {
  name                  = "${var.prefix}sarepstgapilogswebserverlogs"
  storage_account_name  = azurerm_storage_account.storage_rep_stg_api_logs.name
  container_access_type = "blob"
}
