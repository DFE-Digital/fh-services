
# SQL Server Instance
resource "azurerm_mssql_server" "reporting_sql_server" {
  name = "${var.prefix}-as-fh-sql-server-reporting"
  resource_group_name = local.resource_group_name
  location = var.location
  version = "12.0"

  administrator_login = var.sql_server_reporting_user
  administrator_login_password = var.sql_server_reporting_pwd

  azuread_administrator {
    login_username = "s181-growingupwell-Delivery Team USR"
    object_id      = var.service_principals.delivery_team_user_group_object_id
  }
  
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
  cross_tenant_replication_enabled = false
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
