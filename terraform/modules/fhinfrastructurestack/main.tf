
locals {

  resource_group_name = "${var.prefix}-familyhubs"
  
  # App Gateway configuration
  
  appgw_probe_referral_ui_name             = "fh-health-probe-referral-ui"
  appgw_rewrites_referral_ui_name          = "fh-appgw-referral-ui-rewrites"
  appgw_bep_referral_ui_name               = "fh-bep-referral-ui"
  appgw_backend_referral_ui_name           = "fh-backend-referral-ui"
  appgw_listener_http_referral_ui_name     = "fh-listener-http-referral-ui"
  appgw_listener_https_referral_ui_name    = "fh-listener-https-referral-ui"
  appgw_redirect_referral_ui_name = "fh-redirect-referral-ui"
  
  appgw_probe_sd_admin_ui_name = "fh-health-probe-sd-admin-ui"
  appgw_rewrites_sd_admin_ui_name = "fh-appgw-sd-admin-ui-rewrites"
  appgw_bep_sd_admin_ui_name = "fh-bep-sd-admin-ui"
  appgw_backend_sd_admin_ui_name = "fh-backend-sd-admin-ui"
  appgw_listener_http_sd_admin_ui_name = "fh-listener-http-sd-admin-ui"
  appgw_listener_https_sd_admin_ui_name = "fh-listener-https-sd-admin-ui"

  appgw_redirect_sd_admin_ui_name = "fh-redirect-sd-admin-ui"
  appgw_rewrites_sd_ui_name = "fh-appgw-sd-ui-rewrites"
  appgw_bep_sd_ui_name = "fh-bep-sd-ui"
  appgw_backend_sd_ui_name = "fh-backend-sd-ui"
  appgw_probe_sd_ui_name = "fh-health-probe-sd-ui"
  appgw_listener_http_sd_ui_name = "fh-listener-http-sd-ui"
  appgw_listener_https_sd_ui_name = "fh-listener-https-sd-ui"
  appgw_redirect_sd_ui_name = "fh-redirect-sd-ui"

  appgw_ssl_cert_sd_admin_ui_name= "sd-admin-ui-${lower(var.environment)}-cert"
  appgw_ssl_cert_sd_ui_name = "sd-ui-${lower(var.environment)}-cert"
  appgw_ssl_cert_referral_ui_name = "referral-ui-${lower(var.environment)}-cert"
  
  # Storage configuration
  
  account_kind = "StorageV2"
  account_tier = "Standard"
  is_hns_enabled  = false
  account_replication_type  = "LRS"
  access_tier = "Hot"
  min_tls_version = "TLS1_2"
  versioning_enabled = true
  change_feed_enabled = true
  delete_retention_policy_days = 7
  container_delete_retention_policy_days = 7
  public_network_access_enabled_storage = true
  infrastructure_encryption_enabled = true
  
  # SQL
  sal_audit_actions_and_groups = [
    "BATCH_COMPLETED_GROUP",
    "SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP",
    "FAILED_DATABASE_AUTHENTICATION_GROUP"
  ]
  
  # Tags
  tags = {
    "Parent Business" = "Children's Care"
    "Service Offering" = "Growing Up Well"
    "Portfolio" = "Newly Onboarded"
    "Service Line" = "Newly Onboarded"
    "Service" = "Children's Care"
    "Product" = "Growing Up Well"
    "Environment" = var.environment
  }

  # Key vault perms
  principal_certificate_permissions = [
    "Create",
    "Delete",
    "DeleteIssuers",
    "Get",
    "GetIssuers",
    "Import",
    "List",
    "ListIssuers",
    "ManageContacts",
    "ManageIssuers",
    "SetIssuers",
    "Update",
    "Purge",
  ]

  principal_key_permissions = [
    "Backup",
    "Create",
    "Decrypt",
    "Delete",
    "Encrypt",
    "Get",
    "Import",
    "List",
    "Purge",
    "Recover",
    "Restore",
    "Sign",
    "UnwrapKey",
    "Update",
    "Verify",
    "WrapKey",
    "Release",
    "Rotate",
    "GetRotationPolicy",
    "SetRotationPolicy",
  ]

  principal_secret_permissions = [
    "Backup",
    "Delete",
    "Get",
    "List",
    "Purge",
    "Recover",
    "Restore",
    "Set",
  ]

  app_secret_permissions = [
    "Get",
    "List"
  ]

  app_key_permissions = [
    "Get",
    "List",
    "UnwrapKey"
  ]

  referral_app_key_permissions = [
    "Backup",
    "Create",
    "Decrypt",
    "Delete",
    "Encrypt",
    "Get",
    "Import",
    "List",
    "Purge",
    "Recover",
    "Restore",
    "Sign",
    "UnwrapKey",
    "Update",
    "Verify",
    "WrapKey",
  ]
  
  # Database
  database_connection_format = "Server=%s;Database=%s;Authentication=Active Directory Default;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  open_referral_mock_db_connection = format(local.database_connection_format, azurerm_mssql_server.sqlserver.fully_qualified_domain_name, "${var.prefix}-fh-open-referral-mock-db")
  idam_db_connection = format(local.database_connection_format, azurerm_mssql_server.sqlserver.fully_qualified_domain_name, "${var.prefix}-fh-idam-db")
  referral_db_connection = format(local.database_connection_format, azurerm_mssql_server.sqlserver.fully_qualified_domain_name, "${var.prefix}-fh-referral-db")
  service_directory_db_connection = format(local.database_connection_format, azurerm_mssql_server.sqlserver.fully_qualified_domain_name, "${var.prefix}-fh-service-directory-db")
  notification_db_connection = format(local.database_connection_format, azurerm_mssql_server.sqlserver.fully_qualified_domain_name, "${var.prefix}-fh-notification-db")
  report_db_connection = format(local.database_connection_format, azurerm_mssql_server.sqlserver.fully_qualified_domain_name, "${var.prefix}-fh-report-db")
}

# Create App Service Plan
resource "azurerm_service_plan" "apps_plan" {
  name                = "${var.prefix}-asp-familyhubs"
  resource_group_name = local.resource_group_name
  location            = var.location
  os_type             = var.os_type
  sku_name            = var.sku_name
  tags = local.tags
}

# Autoscale Rule for App Services
resource "azurerm_monitor_autoscale_setting" "autoscale" {
  name                = "${var.prefix}-asp-fh-autoscale-rule"
  resource_group_name = local.resource_group_name
  location            = var.location
  target_resource_id  = azurerm_service_plan.apps_plan.id

  profile {
    name = "defaultProfile"

    capacity {
      default = var.autoscale_rule_default_capacity
      minimum = var.autoscale_rule_minimum_capacity
      maximum = var.autoscale_rule_maximum_capacity
    }

    rule {
      metric_trigger {
        metric_name        = "CpuPercentage"
        metric_resource_id = azurerm_service_plan.apps_plan.id
        time_grain         = "PT1M"
        statistic          = "Average"
        time_window        = "PT5M"
        time_aggregation   = "Average"
        operator           = "GreaterThan"
        threshold          = 70
      }

      scale_action {
        direction = "Increase"
        type      = "ChangeCount"
        value     = "1"
        cooldown  = "PT1M"
      }
    }

    rule {
      metric_trigger {
       metric_name        =  "CpuPercentage"
        metric_resource_id = azurerm_service_plan.apps_plan.id
        time_grain         = "PT1M"
        statistic          = "Average"
        time_window        = "PT5M"
        time_aggregation   = "Average"
        operator           = "LessThan"
        threshold          = 20
      }

      scale_action {
        direction = "Decrease"
        type      = "ChangeCount"
        value     = "1"
        cooldown  = "PT1M"
      }
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

# Create App Service for IDAM Maintenance UI
resource "azurerm_windows_web_app" "fh_idam_maintenance_ui" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
    XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
    ASPNETCORE_ENVIRONMENT                      = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE                    = "1"
    APPLICATIONINSIGHTS_CONNECTION_STRING       = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier"       = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix"           = "IDAM-MAINTENANCE-UI"
    "SqlServerCache:Connection"                 = local.idam_db_connection
    "ConnectionStrings:IdamConnection"          = local.idam_db_connection
  }
  name                                          = "${var.prefix}-as-fh-idam-maintenance-ui"
  resource_group_name                           = local.resource_group_name
  location                                      = var.location
  service_plan_id                               = azurerm_service_plan.apps_plan.id
  client_affinity_enabled                       = false
  https_only                                    = true
  identity {
    type                                        = "SystemAssigned"
  }
  site_config {
    always_on                                   = true
    ftps_state                                  = "Disabled"
    health_check_path                           = "/api/health"
    health_check_eviction_time_in_min           = 5 # How long to be removed from LB if unhealthy
    vnet_route_all_enabled                      = "true"
    http2_enabled                               = true
    application_stack {
      current_stack                               = var.current_stack
      dotnet_version                              = var.dotnet_version_general
    }
    ip_restriction {
      name       = "AllowAppAccess"
      priority   = 1
      action     = "Allow"
      ip_address = var.vnetint_address_space[0]
    }
    ip_restriction {
      name       = "DenyPublicAccess"
      priority   = 200
      action     = "Deny"
      ip_address = "0.0.0.0/0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id, logs]
  }
}

# Stop the App Service after creation
resource "null_resource" "stop_idam_maintenance_ui" {
  depends_on = [azurerm_windows_web_app.fh_idam_maintenance_ui]

  provisioner "local-exec" {
    command = <<EOT
      az webapp stop --name "${azurerm_windows_web_app.fh_idam_maintenance_ui.name}" --resource-group "${azurerm_windows_web_app.fh_idam_maintenance_ui.resource_group_name}"
    EOT
  }
}

# Swift Connection for IDAM Maintenance UI
resource "azurerm_app_service_virtual_network_swift_connection" "fh_idam_maintenance_ui" {
  app_service_id = azurerm_windows_web_app.fh_idam_maintenance_ui.id
  subnet_id      = azurerm_subnet.vnetint.id
}

# Create App Service for Referral API
resource "azurerm_windows_web_app" "fh_referral_api" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
    XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
    ASPNETCORE_ENVIRONMENT                      = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE                    = "1"
    APPLICATIONINSIGHTS_CONNECTION_STRING       = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier"       = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix"           = "REFERRAL-API"
    "ConnectionStrings:ReferralConnection"      = local.referral_db_connection
  }
  name                                          = "${var.prefix}-as-fh-referral-api"
  resource_group_name                           = local.resource_group_name
  location                                      = var.location
  service_plan_id                               = azurerm_service_plan.apps_plan.id
  client_affinity_enabled                       = false
  https_only                                    = true
  identity {
    type                                        = "SystemAssigned"
  }
  site_config {
    always_on                                   = true
    ftps_state                                  = "Disabled"
    health_check_path                           = "/api/health"
    http2_enabled                               = true
    health_check_eviction_time_in_min           = 5 # How long to be removed from LB if unhealthy
    application_stack {
      current_stack                               = var.current_stack
      dotnet_version                              = var.dotnet_version_general
    }
    ip_restriction {
      name       = "AllowAppAccess"
      priority   = 1
      action     = "Allow"
      ip_address = var.vnetint_address_space[0]
    }
    ip_restriction {
      name       = "DenyPublicAccess"
      priority   = 200
      action     = "Deny"
      ip_address = "0.0.0.0/0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id, logs]
  }
}

# Swift Connection for Referral API
resource "azurerm_app_service_virtual_network_swift_connection" "fh_referral_api" {
  app_service_id = azurerm_windows_web_app.fh_referral_api.id
  subnet_id      = azurerm_subnet.vnetint.id
}

# Create App Service for Referral UI
resource "azurerm_windows_web_app" "fh_referral_ui" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
    XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
    ASPNETCORE_ENVIRONMENT                      = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE                    = "1"
    APPLICATIONINSIGHTS_CONNECTION_STRING       = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier"       = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix"           = "CONNECT-UI"
    "SqlServerCache:Connection"                 = local.referral_db_connection
    "ConnectionStrings:SharedKernelConnection"  = local.referral_db_connection
  }
  name                                          = "${var.prefix}-as-fh-referral-ui"
  resource_group_name                           = local.resource_group_name
  location                                      = var.location
  service_plan_id                               = azurerm_service_plan.apps_plan.id
  client_affinity_enabled                       = false
  https_only                                    = false # SSL termination at GW
  identity {
    type                                        = "SystemAssigned"
  }
  site_config {
    always_on                                   = true
    ftps_state                                  = "Disabled"
    health_check_path                           = "/api/health"
    health_check_eviction_time_in_min           = 5 # How long to be removed from LB if unhealthy
    vnet_route_all_enabled                      = "true"
    http2_enabled                               = true
    application_stack {
      current_stack                               = var.current_stack
      dotnet_version                              = var.dotnet_version_general
    }
    ip_restriction {
      name       = "AllowAppAccess"
      priority   = 1
      action     = "Allow"
      ip_address = var.vnetint_address_space[0]
    }
    ip_restriction {
      name       = "DenyPublicAccess"
      priority   = 200
      action     = "Deny"
      ip_address = "0.0.0.0/0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id, logs]
  }
}

# Stop the App Service after creation
resource "null_resource" "stop_fh_referral_ui" {
  depends_on = [azurerm_windows_web_app.fh_referral_ui]

  provisioner "local-exec" {
    command = <<EOT
      az webapp stop --name "${azurerm_windows_web_app.fh_referral_ui.name}" --resource-group "${azurerm_windows_web_app.fh_referral_ui.resource_group_name}"
    EOT
  }
}

# Swift Connection for Referral UI
resource "azurerm_app_service_virtual_network_swift_connection" "fh_referral_ui" {
  app_service_id = azurerm_windows_web_app.fh_referral_ui.id
  subnet_id      = azurerm_subnet.vnetint.id
}

# Create App Service for Service Directory API
resource "azurerm_windows_web_app" "fh_sd_api" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
    XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
    ASPNETCORE_ENVIRONMENT                      = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE                    = "1"
    DiagnosticServices_EXTENSION_VERSION        = "~3"
    InstrumentationEngine_EXTENSION_VERSION     = "disabled"
    SnapshotDebugger_EXTENSION_VERSION          = "disabled"
    XDT_MicrosoftApplicationInsights_BaseExtensions = "disabled"
    XDT_MicrosoftApplicationInsights_Java       = "1"
    XDT_MicrosoftApplicationInsights_NodeJS     = "1"
    XDT_MicrosoftApplicationInsights_PreemptSdk = "disabled"
    APPLICATIONINSIGHTS_CONNECTION_STRING       = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier"       = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix"           = "SD-API"
    "ConnectionStrings:ServiceDirectoryConnection" = local.service_directory_db_connection
  }
  name                                          = "${var.prefix}-as-fh-sd-api"
  resource_group_name                           = local.resource_group_name
  location                                      = var.location
  service_plan_id                               = azurerm_service_plan.apps_plan.id
  client_affinity_enabled                       = false
  https_only                                    = true
  identity {
    type                                        = "SystemAssigned"
  }
  site_config {
    always_on                                   = true
    ftps_state                                  = "Disabled"
    health_check_path                           = "/api/health"
    http2_enabled                               = true
    health_check_eviction_time_in_min           = 5 # How long to be removed from LB if unhealthy
    application_stack {
      current_stack                               = var.current_stack
      dotnet_version                              = var.dotnet_version_general
    }
    ip_restriction {
      name       = "AllowAppAccess"
      priority   = 1
      action     = "Allow"
      ip_address = var.vnetint_address_space[0]
    }
    ip_restriction {
      name       = "DenyPublicAccess"
      priority   = 200
      action     = "Deny"
      ip_address = "0.0.0.0/0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id, logs]
  }
}

# Swift Connection for Service Directory API
resource "azurerm_app_service_virtual_network_swift_connection" "fh_sd_api" {
  app_service_id = azurerm_windows_web_app.fh_sd_api.id
  subnet_id      = azurerm_subnet.vnetint.id
}

# Create App Service for Service Directory Admin UI
resource "azurerm_windows_web_app" "fh_sd_admin_ui" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
    XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
    ASPNETCORE_ENVIRONMENT                      = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE                    = "1"
    APPLICATIONINSIGHTS_CONNECTION_STRING       = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier"       = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix"           = "MANAGE-UI"
    "CacheConnection"                           = local.idam_db_connection
  }
  name                                          = "${var.prefix}-as-fh-sd-admin-ui"
  resource_group_name                           = local.resource_group_name
  location                                      = var.location
  service_plan_id                               = azurerm_service_plan.apps_plan.id
  client_affinity_enabled                       = false
  https_only                                    = false # SSL termination at GW
  identity {
    type                                        = "SystemAssigned"
  }
  site_config {
    always_on                                   = true
    ftps_state                                  = "Disabled"
    health_check_path                           = "/api/health"
    health_check_eviction_time_in_min           = 5 # How long to be removed from LB if unhealthy
    vnet_route_all_enabled                      = "true"
    http2_enabled                               = true
    application_stack {
      current_stack                               = var.current_stack
      dotnet_version                              = var.dotnet_version_general
    }
    ip_restriction {
      name       = "AllowAppAccess"
      priority   = 1
      action     = "Allow"
      ip_address = var.vnetint_address_space[0]
    }
    ip_restriction {
      name       = "DenyPublicAccess"
      priority   = 200
      action     = "Deny"
      ip_address = "0.0.0.0/0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id, logs]
  }
}

# Stop the App Service after creation
resource "null_resource" "stop_fh_sd_admin_ui" {
  depends_on = [azurerm_windows_web_app.fh_sd_admin_ui]

  provisioner "local-exec" {
    command = <<EOT
      az webapp stop --name "${azurerm_windows_web_app.fh_sd_admin_ui.name}" --resource-group "${azurerm_windows_web_app.fh_sd_admin_ui.resource_group_name}"
    EOT
  }
}

# Swift Connection for Service Directory Admin UI
resource "azurerm_app_service_virtual_network_swift_connection" "fh_sd_admin_ui" {
  app_service_id = azurerm_windows_web_app.fh_sd_admin_ui.id
  subnet_id      = azurerm_subnet.vnetint.id
}

# Create App Service for IDAM API
resource "azurerm_windows_web_app" "fh_idam_api" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
    XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
    ASPNETCORE_ENVIRONMENT                      = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE                    = "1"
    APPLICATIONINSIGHTS_CONNECTION_STRING       = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier"       = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix"           = "IDAM-API"
    "ConnectionStrings:IdamConnection"          = local.idam_db_connection
  }
  name                                          = "${var.prefix}-as-fh-idam-api"
  resource_group_name                           = local.resource_group_name
  location                                      = var.location
  service_plan_id                               = azurerm_service_plan.apps_plan.id
  client_affinity_enabled                       = false
  https_only                                    = true
  identity {
    type                                        = "SystemAssigned"
  }
  site_config {
    always_on                                   = true
    ftps_state                                  = "Disabled"
    health_check_path                           = "/api/health"
    http2_enabled                               = true
    health_check_eviction_time_in_min           = 5 # How long to be removed from LB if unhealthy
    application_stack {
      current_stack                               = var.current_stack
      dotnet_version                              = var.dotnet_version_general
    }
    ip_restriction {
      name       = "AllowAppAccess"
      priority   = 1
      action     = "Allow"
      ip_address = var.vnetint_address_space[0]
    }
    ip_restriction {
      name       = "DenyPublicAccess"
      priority   = 200
      action     = "Deny"
      ip_address = "0.0.0.0/0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id, logs]
  }
}

# Swift Connection for IDAM API
resource "azurerm_app_service_virtual_network_swift_connection" "fh_idam_api" {
  app_service_id = azurerm_windows_web_app.fh_idam_api.id
  subnet_id      = azurerm_subnet.vnetint.id
}

# Create App Service for Notification API
resource "azurerm_windows_web_app" "fh_notification_api" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION  = "~3"
    XDT_MicrosoftApplicationInsights_Mode       = "Recommended"
    ASPNETCORE_ENVIRONMENT                      = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE                    = "1"
    APPLICATIONINSIGHTS_CONNECTION_STRING       = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier"       = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix"           = "NOTIFICATIONS-API"
    "ConnectionStrings:NotificationConnection"  = local.notification_db_connection
  }
  name                                          = "${var.prefix}-as-fh-notification-api"
  resource_group_name                           = local.resource_group_name
  location                                      = var.location
  service_plan_id                               = azurerm_service_plan.apps_plan.id
  client_affinity_enabled                       = false
  https_only                                    = true
  identity {
    type                                        = "SystemAssigned"
  }
  site_config {
    always_on                                   = true
    ftps_state                                  = "Disabled"
    health_check_path                           = "/api/health"
    http2_enabled                               = true
    health_check_eviction_time_in_min           = 5 # How long to be removed from LB if unhealthy
    application_stack {
      current_stack                               = var.current_stack
      dotnet_version                              = var.dotnet_version_general
    }
    ip_restriction {
      name       = "AllowAppAccess"
      priority   = 1
      action     = "Allow"
      ip_address = var.vnetint_address_space[0]
    }
    ip_restriction {
      name       = "DenyPublicAccess"
      priority   = 200
      action     = "Deny"
      ip_address = "0.0.0.0/0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id, logs]
  }
}

# Swift Connection for Notification API
resource "azurerm_app_service_virtual_network_swift_connection" "fh_notification_api" {
  app_service_id = azurerm_windows_web_app.fh_notification_api.id
  subnet_id      = azurerm_subnet.vnetint.id
}

# Create App Service for open referral API
resource "azurerm_windows_web_app" "open_referral_mock_api_web_app" {
  app_settings = {
    ApplicationInsightsAgent_EXTENSION_VERSION = "~3"
    XDT_MicrosoftApplicationInsights_Mode = "Recommended"
    ASPNETCORE_ENVIRONMENT = var.asp_netcore_environment
    WEBSITE_RUN_FROM_PACKAGE = "1"
    APPLICATIONINSIGHTS_CONNECTION_STRING = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier" = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix" = "MOCK-HSDA-API"
    "ConnectionStrings:HsdaMockResponsesConnection"  = local.open_referral_mock_db_connection
  }
  name = "${var.prefix}-as-fh-open-referral-mock-api"
  resource_group_name = local.resource_group_name
  location = var.location
  service_plan_id = azurerm_service_plan.apps_plan.id
  client_affinity_enabled = false
  https_only = true
  identity {
    type = "SystemAssigned"
  }
  site_config {
    always_on = true
    ftps_state = "Disabled"
    health_check_path = "/"
    health_check_eviction_time_in_min = 5 # How long to be removed from LB if unhealthy
    vnet_route_all_enabled = "true"
    http2_enabled = true
    application_stack {
      current_stack = var.current_stack
      dotnet_version = "v8.0"
    }
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [virtual_network_subnet_id]
  }
}

# Swift Connection for open referral API
resource "azurerm_app_service_virtual_network_swift_connection" "open_referral_mock_api_vnet_swift_connection" {
  app_service_id = azurerm_windows_web_app.open_referral_mock_api_web_app.id
  subnet_id = azurerm_subnet.vnetint.id
}

# WAF Policies
resource "azurerm_web_application_firewall_policy" "ref_ui_appgwwafp" {
  name                = "${var.prefix}-fh-waf-referral-ui"
  resource_group_name = local.resource_group_name
  location            = var.location
  managed_rules {
    exclusion {
      match_variable = "RequestCookieNames"
      selector = "*"
      selector_match_operator = "EqualsAny"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-942-APPLICATION-ATTACK-SQLI"
          excluded_rules = [942440, 942260]
        }
      }
    }
    exclusion {
      match_variable = "RequestArgValues"
      selector = ".png"
      selector_match_operator = "EndsWith"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920300]
        }
      }
    }
    exclusion {
      match_variable = "RequestArgValues"
      selector = ".ico"
      selector_match_operator = "EndsWith"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920300]
        }
      }
    }
    exclusion {
      match_variable = "RequestHeaderNames"
      selector = "host"
      selector_match_operator = "Equals"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920350]
        }
      }
    }
    managed_rule_set {
      type    = "OWASP"
      version = "3.2"
      rule_group_override {
        rule_group_name = "REQUEST-942-APPLICATION-ATTACK-SQLI"
        rule {
          id = "942430"
          enabled = false
        }
        rule {
          id = "942440"
          enabled = false
        }
        rule {
          id = "942450"
          enabled = false
        }
        rule {
          id = "942400"
          enabled = false
        }
      }
      rule_group_override {
        rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
        rule {
          id = "920300"
          enabled = false
        }
      }
      rule_group_override {
        rule_group_name = "REQUEST-931-APPLICATION-ATTACK-RFI"
        rule {
          id = "931130"
          enabled = false
        }
      }
    }
  }
  policy_settings {
    mode = "Prevention"
  }
  tags = local.tags
}

resource "azurerm_web_application_firewall_policy" "sd_admin_ui_appgwwafp" {
  name                = "${var.prefix}-fh-waf-sd-admin-ui"
  resource_group_name = local.resource_group_name
  location            = var.location
  managed_rules {
    exclusion {
      match_variable = "RequestCookieNames"
      selector = "*"
      selector_match_operator = "EqualsAny"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-942-APPLICATION-ATTACK-SQLI"
          excluded_rules = [942440, 942260]
        }
      }
    }
    exclusion {
      match_variable = "RequestArgValues"
      selector = ".png"
      selector_match_operator = "EndsWith"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920300]
        }
      }
    }
    exclusion {
      match_variable = "RequestArgValues"
      selector = ".ico"
      selector_match_operator = "EndsWith"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920300]
        }
      }
    }
    exclusion {
      match_variable = "RequestHeaderNames"
      selector = "host"
      selector_match_operator = "Equals"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920350]
        }
      }
    }
    managed_rule_set {
      type    = "OWASP"
      version = "3.2"
      rule_group_override {
        rule_group_name = "REQUEST-942-APPLICATION-ATTACK-SQLI"
        rule {
          id = "942430"
          enabled = false
        }
        rule {
          id = "942440"
          enabled = false
        }
        rule {
          id = "942450"
          enabled = false
        }
        rule {
          id = "942400"
          enabled = false
        }
      }
      rule_group_override {
        rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
        rule {
          id = "920300"
          enabled = false
        }
      }
      rule_group_override {
        rule_group_name = "REQUEST-931-APPLICATION-ATTACK-RFI"
        rule {
          id = "931130"
          enabled = false
        }
      }
    }
  }

  policy_settings {
    mode = "Prevention"
  }
  tags = local.tags
}

resource "azurerm_web_application_firewall_policy" "sd_ui_appgwwafp" {
  name                = "${var.prefix}-fh-waf-sd-ui"
  resource_group_name = local.resource_group_name
  location            = var.location
  managed_rules {
    exclusion {
      match_variable = "RequestCookieNames"
      selector = "*"
      selector_match_operator = "EqualsAny"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-942-APPLICATION-ATTACK-SQLI"
          excluded_rules = [942440, 942260]
        }
      }
    }
    exclusion {
      match_variable = "RequestArgValues"
      selector = ".png"
      selector_match_operator = "EndsWith"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920300]
        }
      }
    }
    exclusion {
      match_variable = "RequestArgValues"
      selector = ".ico"
      selector_match_operator = "EndsWith"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920300]
        }
      }
    }
    exclusion {
      match_variable = "RequestHeaderNames"
      selector = "host"
      selector_match_operator = "Equals"
      excluded_rule_set {
        rule_group {
          rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
          excluded_rules = [920350]
        }
      }
    }
    managed_rule_set {
      type    = "OWASP"
      version = "3.2"
      rule_group_override {
        rule_group_name = "REQUEST-942-APPLICATION-ATTACK-SQLI"
        rule {
          id = "942430"
          enabled = false
        }
        rule {
          id = "942440"
          enabled = false
        }
        rule {
          id = "942450"
          enabled = false
        }
        rule {
          id = "942400"
          enabled = false
        }
      }
      rule_group_override {
        rule_group_name = "REQUEST-920-PROTOCOL-ENFORCEMENT"
        rule {
          id = "920300"
          enabled = false
        }
      }
      rule_group_override {
        rule_group_name = "REQUEST-931-APPLICATION-ATTACK-RFI"
        rule {
          id = "931130"
          enabled = false
        }
      }
    }
  }
  policy_settings {
    mode = "Prevention"
  }
  tags = local.tags
}

# Application Gateways
resource "azurerm_application_gateway" "ref_ui_app_gateway" {
  name                = "${var.prefix}-fh-appgw-referral-ui"
  firewall_policy_id  =  azurerm_web_application_firewall_policy.ref_ui_appgwwafp.id
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags

  autoscale_configuration {
    min_capacity                = 1
    max_capacity                = 10
  }

  ssl_certificate {
    name     = "${var.prefix}-${local.appgw_ssl_cert_referral_ui_name}"
    data     = var.ssl_cert_path_referral_ui
    password = var.certificate_password
  }

  probe {
    name                        = "${var.prefix}-${local.appgw_probe_referral_ui_name}"
    protocol                    = "Http"
    interval                    = "30"
    timeout                     = "30"
    unhealthy_threshold         = "3"
    pick_host_name_from_backend_http_settings = true
    path                        = "/api/health"
    match {
      status_code               = [ "200-399" ]
    }
  }

  ssl_policy {
    policy_type           = "Predefined"
    policy_name           = "AppGwSslPolicy20220101"
  }

  rewrite_rule_set {
     name                 = "${var.prefix}-${local.appgw_rewrites_referral_ui_name}"
     rewrite_rule {
        name              = "NewRewrite"
        rule_sequence     = "100"       
     response_header_configuration {
       header_name        = "Server"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "X-Powered-By"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "Strict-Transport-Security"
       header_value       = "max-age=31536000; includeSubDomains; preload"
      }
     request_header_configuration {
       header_name        = "X-Forwarded-Host"
       header_value       = var.connect_domain
      }
     }
   }

  backend_address_pool {
      name                = "${var.prefix}-${local.appgw_bep_referral_ui_name}"
      fqdns               = [ azurerm_windows_web_app.fh_referral_ui.default_hostname ]
  }

  backend_http_settings {
    name                = "${var.prefix}-${local.appgw_backend_referral_ui_name}"
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 30
    path                  = "/"
    connection_draining {
      drain_timeout_sec = 60
      enabled           = true
    }
    probe_name          = "${var.prefix}-${local.appgw_probe_referral_ui_name}"
    pick_host_name_from_backend_address = true
  }

  gateway_ip_configuration {
    name      = "appGatewayIpConfig"
    subnet_id = azurerm_subnet.applicationgateway.id
  }

  frontend_port {
    name = "port_80"
    port = 80
  }

  frontend_port {
    name = "port_443"
    port = 443
  }

  frontend_ip_configuration {
    name                 = "appGwPublicFrontendIp"
    public_ip_address_id = data.azurerm_public_ip.referral_ui_public_ip.id
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_http_referral_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_80"
    protocol                       = "Http"
  }
  
  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_https_referral_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_443"
    protocol                       = "Https"
    ssl_certificate_name           = "${var.prefix}-${local.appgw_ssl_cert_referral_ui_name}"
    custom_error_configuration {
        status_code           = "HttpStatus403"
        custom_error_page_url = "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_referral_ui.name}/error403.html"
    }
    custom_error_configuration {
        status_code           = "HttpStatus502"
        custom_error_page_url =  "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_referral_ui.name}/error502.html"
    }  
  }

  redirect_configuration {
    name                 = "${var.prefix}-${local.appgw_redirect_referral_ui_name}"
    redirect_type        = "Permanent"
    include_path         = true
    include_query_string = true
    target_listener_name = "${var.prefix}-${local.appgw_listener_https_referral_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-https-referral-ui"
    backend_address_pool_name  = "${var.prefix}-${local.appgw_bep_referral_ui_name}"
    backend_http_settings_name = "${var.prefix}-${local.appgw_backend_referral_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_https_referral_ui_name}"
    priority                   = 1
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_referral_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-http-referral-ui"
    redirect_configuration_name = "${var.prefix}-${local.appgw_redirect_referral_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_http_referral_ui_name}"
    priority                   = 2
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_referral_ui_name}"
  }

  sku {
    name     = "WAF_v2"
    tier     = "WAF_v2"
  }
    waf_configuration {
    enabled          = true
    firewall_mode    = "Detection"
    rule_set_version = "3.2"
  }
  enable_http2       = true
  
  lifecycle {
    ignore_changes = [request_routing_rule]
  }
}

resource "azurerm_application_gateway" "sd_admin_ui_app_gateway" {
  name                = "${var.prefix}-fh-appgw-sd-admin-ui"
  firewall_policy_id  =  azurerm_web_application_firewall_policy.sd_admin_ui_appgwwafp.id
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags

  autoscale_configuration {
    min_capacity                = 1
    max_capacity                = 10
  }

  ssl_certificate {
    name     = "${var.prefix}-${local.appgw_ssl_cert_sd_admin_ui_name}"
    data     = var.ssl_cert_path_sd_admin_ui
    password = var.certificate_password
  }

  probe {
    name                        = "${var.prefix}-${local.appgw_probe_sd_admin_ui_name}"
    protocol                    = "Http"
    interval                    = "30"
    timeout                     = "30"
    unhealthy_threshold         = "3"
    pick_host_name_from_backend_http_settings = true
    path                        = "/api/health"
    match {
      status_code               = [ "200-399" ]
    }
  }

  ssl_policy {
    policy_type           = "Predefined"
    policy_name           = "AppGwSslPolicy20220101"
  }

  rewrite_rule_set {
     name                 = "${var.prefix}-${local.appgw_rewrites_sd_admin_ui_name}"
     rewrite_rule {
        name              = "NewRewrite"
        rule_sequence     = "100"
            
     response_header_configuration {
       header_name        = "Server"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "X-Powered-By"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "Strict-Transport-Security"
       header_value       = "max-age=31536000; includeSubDomains; preload"
      }
     request_header_configuration {
       header_name        = "X-Forwarded-Host"
       header_value       = var.manage_domain
      }
     }
   }

  backend_address_pool {
      name                = "${var.prefix}-${local.appgw_bep_sd_admin_ui_name}"
      fqdns               = [ azurerm_windows_web_app.fh_sd_admin_ui.default_hostname ]
  }

  backend_http_settings {
    name                = "${var.prefix}-${local.appgw_backend_sd_admin_ui_name}"
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 1200
    path                  = "/"
    connection_draining {
      drain_timeout_sec = 60
      enabled           = true
    }
    probe_name          = "${var.prefix}-${local.appgw_probe_sd_admin_ui_name}"
    pick_host_name_from_backend_address = true
  }

  gateway_ip_configuration {
    name      = "appGatewayIpConfig"
    subnet_id = azurerm_subnet.applicationgateway.id
  }

   frontend_port {
    name = "port_80"
    port = 80
  }

  frontend_port {
    name = "port_443"
    port = 443
  }
  
  frontend_ip_configuration {
    name                 = "appGwPublicFrontendIp"
    public_ip_address_id = data.azurerm_public_ip.sd_admin_ui_public_ip.id
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_http_sd_admin_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_80"
    protocol                       = "Http"
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_https_sd_admin_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_443"
    protocol                       = "Https"
    ssl_certificate_name           = "${var.prefix}-${local.appgw_ssl_cert_sd_admin_ui_name}"
	  custom_error_configuration {
        status_code           = "HttpStatus403"
        custom_error_page_url = "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_sd_admin_ui.name}/error403.html"
    }
    custom_error_configuration {
        status_code           = "HttpStatus502"
        custom_error_page_url =  "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_sd_admin_ui.name}/error502.html"
    } 
  }

  redirect_configuration {
    name                 = "${var.prefix}-${local.appgw_redirect_sd_admin_ui_name}"
    redirect_type        = "Permanent"
    include_path         = true
    include_query_string = true
    target_listener_name = "${var.prefix}-${local.appgw_listener_https_sd_admin_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-https-sd-admin-ui"
    backend_address_pool_name  = "${var.prefix}-${local.appgw_bep_sd_admin_ui_name}"
    backend_http_settings_name = "${var.prefix}-${local.appgw_backend_sd_admin_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_https_sd_admin_ui_name}"
    priority                   = 1
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_admin_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-http-sd-admin-ui"
    redirect_configuration_name = "${var.prefix}-${local.appgw_redirect_sd_admin_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_http_sd_admin_ui_name}"
    priority                   = 2
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_admin_ui_name}"
  }

  sku {
    name     = "WAF_v2"
    tier     = "WAF_v2"
  }
    waf_configuration {
    enabled          = true
    firewall_mode    = "Detection"
    rule_set_version = "3.2"
  }
  enable_http2       = true
}

resource "azurerm_application_gateway" "sd_ui_app_gateway" {
  name                = "${var.prefix}-fh-appgw-sd-ui"
  firewall_policy_id  =  azurerm_web_application_firewall_policy.sd_ui_appgwwafp.id
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags

  autoscale_configuration {
    min_capacity                = 1
    max_capacity                = 10
  }

  ssl_certificate {
    name     = "${var.prefix}-${local.appgw_ssl_cert_sd_ui_name}"
    data     = var.ssl_cert_path_sd_ui
    password = var.certificate_password
  }

  probe {
    name                        = "${var.prefix}-${local.appgw_probe_sd_ui_name}"
    protocol                    = "Http"
    interval                    = "30"
    timeout                     = "30"
    unhealthy_threshold         = "3"
    pick_host_name_from_backend_http_settings = true
    path                                      = "/api/health"
    match {
      status_code               = [ "200-399" ]
    }
  }

  ssl_policy {
    policy_type           = "Predefined"
    policy_name           = "AppGwSslPolicy20220101"
  }

  rewrite_rule_set {
     name                 = "${var.prefix}-${local.appgw_rewrites_sd_ui_name}"
     rewrite_rule {
        name              = "NewRewrite"
        rule_sequence     = "100"
             
     response_header_configuration {
       header_name        = "Server"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "X-Powered-By"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "Strict-Transport-Security"
       header_value       = "max-age=31536000; includeSubDomains; preload"
      }
     request_header_configuration {
       header_name        = "X-Forwarded-Host"
       header_value       = var.find_domain
      }
     }
   }

  backend_address_pool {
    name                = "${var.prefix}-${local.appgw_bep_sd_ui_name}"
    ip_addresses = [var.private_endpoint_ip_address.service_directory_ui]
  }
  
  backend_http_settings {
    name                = "${var.prefix}-${local.appgw_backend_sd_ui_name}"
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 30
    path                  = "/"
    connection_draining {
      drain_timeout_sec = 60
      enabled           = true
    }
    probe_name          = "${var.prefix}-${local.appgw_probe_sd_ui_name}"
    pick_host_name_from_backend_address = true
  }

  gateway_ip_configuration {
    name      = "appGatewayIpConfig"
    subnet_id = azurerm_subnet.applicationgateway.id
  }

  frontend_port {
    name = "port_80"
    port = 80
  }

  frontend_port {
    name = "port_443"
    port = 443
  }

  frontend_ip_configuration {
    name                 = "appGwPublicFrontendIp"
    public_ip_address_id = data.azurerm_public_ip.sd_ui_public_ip.id
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_http_sd_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_80"
    protocol                       = "Http"
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_https_sd_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_443"
    protocol                       = "Https"
    ssl_certificate_name           = "${var.prefix}-${local.appgw_ssl_cert_sd_ui_name}"
    custom_error_configuration {
        status_code           = "HttpStatus502"
        custom_error_page_url =  "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_sd_ui.name}/error502.html"
    }
  }

  redirect_configuration {
    name                 = "${var.prefix}-${local.appgw_redirect_sd_ui_name}"
    redirect_type        = "Permanent"
    include_path         = true
    include_query_string = true
    target_listener_name = "${var.prefix}-${local.appgw_listener_https_sd_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-https-sd-ui"
    backend_address_pool_name  = "${var.prefix}-${local.appgw_bep_sd_ui_name}"
    backend_http_settings_name = "${var.prefix}-${local.appgw_backend_sd_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_https_sd_ui_name}"
    priority                   = 1
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-http-sd-ui"
    redirect_configuration_name = "${var.prefix}-${local.appgw_redirect_sd_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_http_sd_ui_name}"
    priority                   = 2
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_ui_name}"
  }

  sku {   
    name     = "WAF_v2"
    tier     = "WAF_v2"
  }
    waf_configuration {
    enabled          = true
    firewall_mode    = "Detection"
    rule_set_version = "3.2"
  }
  enable_http2       = true
}

resource "azurerm_monitor_diagnostic_setting" "sd_admin_ui_gw_law_logs" {
  name = "LAW_Profile"
  target_resource_id = azurerm_application_gateway.sd_admin_ui_app_gateway.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.app_services.id
  log_analytics_destination_type = "Dedicated"
  enabled_log {
    category_group = "allLogs"
  }
  metric {
    category = "AllMetrics"
    enabled = false
  }
}

resource "azurerm_monitor_diagnostic_setting" "sd_ui_gw_law_logs" {
  name = "LAW_Profile"
  target_resource_id = azurerm_application_gateway.sd_ui_app_gateway.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.app_services.id
  log_analytics_destination_type = "Dedicated"
  enabled_log {
    category_group = "allLogs"
  }
  metric {
    category = "AllMetrics"
    enabled = false
  }
}

resource "azurerm_monitor_diagnostic_setting" "ref_ui_gw_law_logs" {
  name = "LAW_Profile"
  target_resource_id = azurerm_application_gateway.ref_ui_app_gateway.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.app_services.id
  log_analytics_destination_type = "Dedicated"
  enabled_log {
    category_group = "allLogs"
  }
  metric {
    category = "AllMetrics"
    enabled = false
  }
}

# Key Vaults, Secrets, Certs & Keys
data "azurerm_client_config" "current" {}

resource "azurerm_key_vault" "kv2" {
  depends_on = [
    azurerm_windows_web_app.fh_referral_api,
    azurerm_windows_web_app.open_referral_mock_api_web_app,
    azurerm_windows_web_app.fh_notification_api,
    azurerm_windows_web_app.fh_idam_api,
    azurerm_windows_web_app.fh_idam_maintenance_ui,
    azurerm_windows_web_app.fh_referral_ui,
    azurerm_windows_web_app.fh_report_api,
    azurerm_windows_web_app.fh_sd_api,
    azurerm_windows_web_app.fh_sd_admin_ui,
    azurerm_windows_function_app.open_referral_function_app
  ]
  name                        = "${var.prefix}-kv-fh-admin"
  resource_group_name         = local.resource_group_name
  location                    = var.location
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 90
  purge_protection_enabled    = true
  sku_name                    = "standard"
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.reader_usr_group_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.delivery_team_user_group_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.ado_enterprise_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.github_enterprise_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_referral_api.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.open_referral_mock_api_web_app.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_notification_api.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_idam_api.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_idam_maintenance_ui.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_referral_ui.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_report_api.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_sd_api.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_sd_admin_ui.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_function_app.open_referral_function_app.identity.0.principal_id
    secret_permissions = local.app_secret_permissions
  }
  tags = local.tags
}

resource "azurerm_key_vault" "kv3" {
  depends_on = [ local.resource_group_name]
  name                        = "${var.prefix}-kv-fh-referral"
  resource_group_name         = local.resource_group_name
  location                    = var.location
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 90
  purge_protection_enabled    = true
  sku_name                    = "standard"
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.reader_usr_group_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.delivery_team_user_group_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.ado_enterprise_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_referral_ui.identity.0.principal_id
    key_permissions = local.app_key_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.github_enterprise_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  tags = local.tags
}

resource "azurerm_key_vault_key" "kv3k1" {
  name         = "${var.prefix}-data-protection-key"
  key_vault_id = azurerm_key_vault.kv3.id
  depends_on   = [ azurerm_key_vault.kv3 ]
  key_type     = "RSA"
  key_size     = 2048
  tags = local.tags

  key_opts = [
    "decrypt",
    "encrypt",
    "sign",
    "unwrapKey",
    "verify",
    "wrapKey",
  ]
}

resource "azurerm_key_vault" "kv5" {
  depends_on = [ local.resource_group_name]
  name                        = "${var.prefix}-kv-fh-idam"
  resource_group_name         = local.resource_group_name
  location                    = var.location
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 90
  purge_protection_enabled    = true
  sku_name                    = "standard"
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.reader_usr_group_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.delivery_team_user_group_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.ado_enterprise_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_sd_admin_ui.identity.0.principal_id
    key_permissions = local.referral_app_key_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = azurerm_windows_web_app.fh_referral_ui.identity.0.principal_id
    key_permissions = local.referral_app_key_permissions
  }
  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = var.service_principals.github_enterprise_object_id
    certificate_permissions = local.principal_certificate_permissions
    key_permissions = local.principal_key_permissions
    secret_permissions = local.principal_secret_permissions
  }
  tags = local.tags
}

# SQL Server VA Microsoft Defender
resource "azurerm_mssql_server_security_alert_policy" "sqlserver_security_policy" {
  resource_group_name = local.resource_group_name
  server_name         = azurerm_mssql_server.sqlserver.name
  state               = "Enabled"
}

resource "azurerm_mssql_server_vulnerability_assessment" "sqlserver_db_vulnerability_assessment" {
  server_security_alert_policy_id = azurerm_mssql_server_security_alert_policy.sqlserver_security_policy.id
  storage_container_path          = "${azurerm_storage_account.storage_db_logs.primary_blob_endpoint}${azurerm_storage_container.container_db_va_logs.name}/"
  storage_account_access_key      = azurerm_storage_account.storage_db_logs.primary_access_key

  recurring_scans {
    enabled                   = true
    email_subscription_admins = true
    emails = [
      var.slack_support_channel_email
    ]
  }
}

# SQL Server Instance
resource "azurerm_mssql_server" "sqlserver" {
  name = "${var.prefix}-as-fh-sql-server"
  resource_group_name = local.resource_group_name
  location = var.location
  version = "12.0"

  administrator_login = var.sql_server_user
  administrator_login_password = var.sql_server_pwd

  azuread_administrator {
    login_username = "s181-growingupwell-Delivery Team USR"
    object_id      = var.service_principals.delivery_team_user_group_object_id
  }
  
  tags = local.tags
}

resource "azurerm_mssql_server_extended_auditing_policy" "sql_server_auditing_policy" { 
  server_id = azurerm_mssql_server.sqlserver.id
  log_monitoring_enabled = true
  audit_actions_and_groups = local.sal_audit_actions_and_groups
}

resource "azurerm_monitor_diagnostic_setting" "sql_server_diagnostics" {
  name = "sql-server-diagnostics"
  target_resource_id = "${azurerm_mssql_server.sqlserver.id}/databases/master"
  log_analytics_workspace_id = azurerm_log_analytics_workspace.app_services.id
  enabled_log {
    category_group = "AllLogs"
  }
  metric {
    category = "Basic"
    enabled  = false
  }
  metric {
    category = "InstanceAndAppAdvanced"
    enabled  = false
  }
  metric {
   category = "WorkloadManagement" 
   enabled  = false
  }
}

# SQL Server Databases
resource "azurerm_mssql_database" "referral_serverless_db" {
    name                        = "${var.prefix}-fh-referral-db"
    server_id                   = azurerm_mssql_server.sqlserver.id
    collation                   = "SQL_Latin1_General_CP1_CI_AS"

    max_size_gb                 = 1
    sku_name                    = "S0"
    zone_redundant              = false
    storage_account_type        = "Local"

    short_term_retention_policy {
        retention_days = "7"
        backup_interval_in_hours = "24"
    }

    long_term_retention_policy {
      monthly_retention = "P1M" # Retain for a month
    }

    threat_detection_policy {
        disabled_alerts      = []
        email_account_admins = "Disabled"
        email_addresses      = []
        retention_days       = 0
        state                = "Disabled"
    }

    tags = local.tags
}

resource "azurerm_mssql_database" "idam_serverless_db" {
    name                        = "${var.prefix}-fh-idam-db"
    server_id                   = azurerm_mssql_server.sqlserver.id
    collation                   = "SQL_Latin1_General_CP1_CI_AS"

    max_size_gb                 = 1
    sku_name                    = "S0"
    zone_redundant              = false
    storage_account_type        = "Local"

    short_term_retention_policy {
        retention_days = "7"
        backup_interval_in_hours = "24"
    }

    long_term_retention_policy {
        monthly_retention = "P1M" # Retain for a month
    }

    threat_detection_policy {
        disabled_alerts      = []
        email_account_admins = "Disabled"
        email_addresses      = []
        retention_days       = 0
        state                = "Disabled"
    }

    tags = local.tags
}

resource "azurerm_mssql_database" "sd_serverless_db" {
    name                        = "${var.prefix}-fh-service-directory-db"
    server_id                   = azurerm_mssql_server.sqlserver.id
    collation                   = "SQL_Latin1_General_CP1_CI_AS"

    max_size_gb                 = 1
    sku_name                    = "S0"
    zone_redundant              = false
    storage_account_type        = "Local"

    short_term_retention_policy {
        retention_days = "7"
        backup_interval_in_hours = "24"
    }

    long_term_retention_policy {
      monthly_retention = "P1M" # Retain for a month
    }

    threat_detection_policy {
        disabled_alerts      = []
        email_account_admins = "Disabled"
        email_addresses      = []
        retention_days       = 0
        state                = "Disabled"
    }

    tags = local.tags
}

resource "azurerm_mssql_database" "notification_serverless_db" {
    name                        = "${var.prefix}-fh-notification-db"
    server_id                   = azurerm_mssql_server.sqlserver.id
    collation                   = "SQL_Latin1_General_CP1_CI_AS"

    max_size_gb                 = 1
    sku_name                    = "S0"
    zone_redundant              = false
    storage_account_type        = "Local"

    short_term_retention_policy {
        retention_days = "7"
        backup_interval_in_hours = "24"
    }

    long_term_retention_policy {
      monthly_retention = "P1M" # Retain for a month
    }

    threat_detection_policy {
        disabled_alerts      = []
        email_account_admins = "Disabled"
        email_addresses      = []
        retention_days       = 0
        state                = "Disabled"
    }

    tags = local.tags
}

resource "azurerm_mssql_database" "open_referral_mock_serverless_db" {
  name = "${var.prefix}-fh-open-referral-mock-db"
  server_id = azurerm_mssql_server.sqlserver.id
  collation = "SQL_Latin1_General_CP1_CI_AS"
  max_size_gb = 1
  sku_name = "S0"
  zone_redundant = false
  storage_account_type = "Local"

  short_term_retention_policy {
    retention_days = "7"
    backup_interval_in_hours = "24"
  }

  long_term_retention_policy {
    monthly_retention = "P1M" # Retain for a month
  }

  threat_detection_policy {
    disabled_alerts      = []
    email_account_admins = "Disabled"
    email_addresses      = []
    retention_days       = 0
    state                = "Disabled"
  }

  tags = local.tags
}

# Private DNS Zone - App Services
resource "azurerm_private_dns_zone" "appservices" {
  name                = "privatelink.azurewebsites.net"
  resource_group_name = local.resource_group_name
  soa_record {
    email           = "privatelink.azurewebsites.net"
    expire_time     = "2419200"
    minimum_ttl     = "10"
    ttl             = "3600"
    refresh_time    = "3600"
    retry_time      = "300"
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

# Private DNS Zone - SQL Server
resource "azurerm_private_dns_zone" "sqlserver" {
  name                = "privatelink.database.windows.net"
  resource_group_name = local.resource_group_name
  soa_record {
    email           = "privatelink.database.windows.net"
    expire_time     = "2419200"
    minimum_ttl     = "10"
    ttl             = "3600"
    refresh_time    = "3600"
    retry_time      = "300"
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

# vNET Link - App Services
resource "azurerm_private_dns_zone_virtual_network_link" "vnet_app_services_dns_link" {
  name                  = "${var.prefix}-as-fh-vn-link"
  resource_group_name   = local.resource_group_name
  private_dns_zone_name = azurerm_private_dns_zone.appservices.name
  virtual_network_id    = azurerm_virtual_network.vnet.id
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

# vNET Link - SQL Server
resource "azurerm_private_dns_zone_virtual_network_link" "vnet_sql_server_services_dns_link" {
  name                  = "${var.prefix}-sql-fh-vn-link"
  resource_group_name   = local.resource_group_name
  private_dns_zone_name = azurerm_private_dns_zone.sqlserver.name
  virtual_network_id    = azurerm_virtual_network.vnet.id
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

# Private DNS Zone - App Services A Records
resource "azurerm_private_dns_a_record" "as_sqlserver" {
  name                = "${var.prefix}-as-fh-sql-server"
  zone_name           = azurerm_private_dns_zone.appservices.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.sql_server]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

# Private DNS Zone - SQL Server A Records
resource "azurerm_private_dns_a_record" "sql_idam_api" {
  name                = "${var.prefix}-as-fh-idam-api"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.idam_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_referral_api" {
  name                = "${var.prefix}-as-fh-referral-api"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.referral_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}


resource "azurerm_private_dns_a_record" "sql_idam_api_scm" {
  name                = "${var.prefix}-as-fh-idam-api.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.idam_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_referral_api_scm" {
  name                = "${var.prefix}-as-fh-referral-api.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.referral_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_referral_ui" {
  name                = "${var.prefix}-as-fh-referral-ui"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.referral_ui]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_referral_ui_scm" {
  name                = "${var.prefix}-as-fh-referral-ui.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.referral_ui]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_sd_api" {
  name                = "${var.prefix}-as-fh-sd-api"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.service_directory_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_sd_api_scm" {
  name                = "${var.prefix}-as-fh-sd-api.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.service_directory_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_sd_admin_ui" {
  name                = "${var.prefix}-as-fh-sd-admin-ui"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.service_directory_admin_ui]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_sd_admin_ui_scm" {
  name                = "${var.prefix}-as-fh-sd-admin-ui.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.service_directory_admin_ui]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_notification_api" {
  name                = "${var.prefix}-as-fh-notification-api"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.notification_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_notification_api_scm" {
  name                = "${var.prefix}-as-fh-notification-api.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.notification_api]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_idam_maintenance_ui" {
  name                = "${var.prefix}-as-fh-idam-maintenance-ui"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.idam_maintenance_ui]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_idam_maintenance_ui_scm" {
  name                = "${var.prefix}-as-fh-idam-maintenance-ui.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.idam_maintenance_ui]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_open_referral_mock" {
  name                = "${var.prefix}-as-fh-open-referral-mock"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.open_referral_mock]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

resource "azurerm_private_dns_a_record" "sql_open_referral_mock_scm" {
  name                = "${var.prefix}-as-fh-open-referral-mock.scm"
  zone_name           = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl                 = "10"
  records             = [var.private_endpoint_ip_address.open_referral_mock]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}

# Private Endpoint 1: ReferralAPI
resource "azurerm_private_endpoint" "referralapi" {
  name                = "${var.prefix}-as-fh-referral-api"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.pvtendpoint.id
  custom_network_interface_name = "${var.prefix}-as-fh-referral-api-nic"

  ip_configuration {
    name                       = "${var.prefix}-as-fh-referral-api"
    private_ip_address         = "${var.private_endpoint_ip_address.referral_api}"
    subresource_name           = "sites" 
  }

 private_service_connection {
    name                           = "${var.prefix}-pvtendpt-referral-api"
    private_connection_resource_id = azurerm_windows_web_app.fh_referral_api.id
    is_manual_connection           = false
    subresource_names              = [ "sites" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.appservices.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
  }
  tags = local.tags
}


# Private Endpoint 2: ReferralUI
resource "azurerm_private_endpoint" "referralui" {
  name                = "${var.prefix}-as-fh-referral-ui"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.pvtendpoint.id
  custom_network_interface_name = "${var.prefix}-as-fh-referral-ui-nic"

  ip_configuration {
    name                       = "${var.prefix}-as-fh-referral-ui"
    private_ip_address         = "${var.private_endpoint_ip_address.referral_ui}"
    subresource_name           = "sites" 
  }

  private_service_connection {
    name                           = "${var.prefix}-pvtendpt-referral-ui"
    private_connection_resource_id = azurerm_windows_web_app.fh_referral_ui.id
    is_manual_connection           = false
    subresource_names              = [ "sites" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.appservices.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
  }
  tags = local.tags
}

# Private Endpoint 3: ServiceDirectoryAPI
resource "azurerm_private_endpoint" "sdapi" {
  name                = "${var.prefix}-as-fh-sd-api"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.pvtendpoint.id
  custom_network_interface_name = "${var.prefix}-as-fh-sd-api-nic"

  ip_configuration {
    name                       = "${var.prefix}-as-fh-sd-api"
    private_ip_address         = "${var.private_endpoint_ip_address.service_directory_api}"
    subresource_name           = "sites" 
  }

  private_service_connection {
    name                           = "${var.prefix}-pvtendpt-sd-api"
    private_connection_resource_id = azurerm_windows_web_app.fh_sd_api.id
    is_manual_connection           = false
    subresource_names              = [ "sites" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.appservices.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
  }
  tags = local.tags
}

# Private Endpoint 5: ServiceDirectoryAdminUI
resource "azurerm_private_endpoint" "sdadminui" {
  name                = "${var.prefix}-as-fh-sd-admin-ui"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.pvtendpoint.id
  custom_network_interface_name = "${var.prefix}-as-fh-sd-admin-ui-nic"

   ip_configuration {
    name                       = "${var.prefix}-as-fh-sd-admin-ui"
    private_ip_address         = "${var.private_endpoint_ip_address.service_directory_admin_ui}"
    subresource_name           = "sites" 
  }

  private_service_connection {
    name                           = "${var.prefix}-pvtendpt-sd-admin-ui"
    private_connection_resource_id = azurerm_windows_web_app.fh_sd_admin_ui.id
    is_manual_connection           = false
    subresource_names              = [ "sites" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.appservices.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
  }
  tags = local.tags
}

# Private Endpoint 6: NotificationAPI
resource "azurerm_private_endpoint" "notificationapi" {
  name                = "${var.prefix}-as-fh-notification-api"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.pvtendpoint.id
  custom_network_interface_name = "${var.prefix}-as-fh-notification-api-nic"

   ip_configuration {
    name                       = "${var.prefix}-as-fh-notification-api"
    private_ip_address         = "${var.private_endpoint_ip_address.notification_api}"
    subresource_name           = "sites" 
  }

  private_service_connection {
    name                           = "${var.prefix}-pvtendpt-notification-api"
    private_connection_resource_id = azurerm_windows_web_app.fh_notification_api.id
    is_manual_connection           = false
    subresource_names              = [ "sites" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.appservices.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
  }
  tags = local.tags
}

# Private Endpoint 8: Referrals Dashboard UI
# REMOVED

# Private Endpoint 9: IDAM API
resource "azurerm_private_endpoint" "idamapi" {
  name                = "${var.prefix}-as-fh-idam-api"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.pvtendpoint.id
  custom_network_interface_name = "${var.prefix}-as-fh-idam-api-nic"

  ip_configuration {
    name                       = "${var.prefix}-as-fh-idam-api"
    private_ip_address         = "${var.private_endpoint_ip_address.idam_api}"
    subresource_name           = "sites" 
  }

 private_service_connection {
    name                           = "${var.prefix}-pvtendpt-idam-api"
    private_connection_resource_id = azurerm_windows_web_app.fh_idam_api.id
    is_manual_connection           = false
    subresource_names              = [ "sites" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.appservices.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
  }
  tags = local.tags
}

# Private Endpoint 10: SQL Sever
resource "azurerm_private_endpoint" "sqlserver" {
  name                = "${var.prefix}-as-fh-sql-server"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.sqlserver.id
  custom_network_interface_name = "${var.prefix}-as-fh-sql-server-nic"

  ip_configuration {
    name                       = "${var.prefix}-as-fh-sql-server"
    private_ip_address         = "${var.private_endpoint_ip_address.sql_server}"
    subresource_name           = "sqlserver" 
  }

 private_service_connection {
    name                           = "${var.prefix}-pvtendpt-sql-server"
    private_connection_resource_id = azurerm_mssql_server.sqlserver.id
    is_manual_connection           = false
    subresource_names              = [ "sqlserver" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.sqlserver.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.sqlserver.id ]
  }
  tags = local.tags
}

# Private Endpoint 11: Open Referral Mock API
resource "azurerm_private_endpoint" "open_referral_mock_private_endpoint" {
  name                = "${var.prefix}-as-fh-open-referral-mock"
  location            = var.location
  resource_group_name = local.resource_group_name
  subnet_id           = azurerm_subnet.pvtendpoint.id
  custom_network_interface_name = "${var.prefix}-as-fh-open-referral-mock-nic"

  ip_configuration {
    name                       = "${var.prefix}-as-fh-open-referral-mock"
    private_ip_address         = var.private_endpoint_ip_address.open_referral_mock
    subresource_name           = "sites"
  }

  private_service_connection {
    name                           = "${var.prefix}-pvtendpt-open-referral_mock"
    private_connection_resource_id = azurerm_windows_web_app.open_referral_mock_api_web_app.id
    is_manual_connection           = false
    subresource_names              = [ "sites" ]
  }

  private_dns_zone_group {
    name                 = azurerm_private_dns_zone.appservices.name
    private_dns_zone_ids = [ azurerm_private_dns_zone.appservices.id ]
  }
  tags = local.tags
}

# Public IP
data "azurerm_public_ip" "referral_ui_public_ip" {
  name                      = "${var.prefix}-fh-pip-referral-ui"
  resource_group_name       = local.resource_group_name
}

data "azurerm_public_ip" "sd_admin_ui_public_ip" {
  name                      = "${var.prefix}-fh-pip-sd-admin-ui"
  resource_group_name       = local.resource_group_name
}

data "azurerm_public_ip" "sd_ui_public_ip" {
  name                      = "${var.prefix}-fh-pip-sd-ui"
  resource_group_name       = local.resource_group_name
}

# Storage Accounts

resource "azurerm_storage_account" "storage_appgw_errorpage" {
  name                      			    = "${var.prefix}saappgwerror"
  resource_group_name       			    = local.resource_group_name
  location                  			    = var.location
  account_tier              			    = local.account_tier
  account_kind              			    = local.account_kind
  access_tier               			    = local.access_tier
  min_tls_version           			    = local.min_tls_version
  public_network_access_enabled 		    = local.public_network_access_enabled_storage
  account_replication_type  			    = local.account_replication_type
  infrastructure_encryption_enabled 	= local.infrastructure_encryption_enabled
  cross_tenant_replication_enabled          = false
  provisioner "local-exec" {
    command = "az storage account update --name ${self.name} --resource-group ${self.resource_group_name} --key-exp-days 365"
    interpreter = ["pwsh", "-Command"]
  }
  blob_properties {
    versioning_enabled     				    = local.versioning_enabled
    change_feed_enabled    				    = local.change_feed_enabled
    delete_retention_policy {
      days                 				    = local.delete_retention_policy_days
    }
    container_delete_retention_policy {
      days                 				    = local.container_delete_retention_policy_days
    }
  }
  tags = local.tags
}

resource "azurerm_storage_container" "container_appgw_referral_ui" {
  name                  				= "${var.prefix}sacontappgwrefui"
  storage_account_id  				= azurerm_storage_account.storage_appgw_errorpage.id
  container_access_type 				= "blob"
}

resource "azurerm_storage_blob" "blob_appgw_referral_ui_error502" {
  name                   				= "error502.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_referral_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_referral_ui}/error502.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_referral_ui}/error502.html")
}

resource "azurerm_storage_blob" "blob_appgw_referral_ui_error403" {
  name                   				= "error403.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_referral_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_referral_ui}/error403.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_referral_ui}/error403.html")
}

resource "azurerm_storage_container" "container_appgw_sd_admin_ui" {
  name                  				= "${var.prefix}sacontappgwsdadminui"
  storage_account_id  				= azurerm_storage_account.storage_appgw_errorpage.id
  container_access_type 				= "blob"
}

resource "azurerm_storage_blob" "blob_appgw_sd_admin_ui_error502" {
  name                   				= "error502.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_sd_admin_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_sd_admin_ui}/error502.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_sd_admin_ui}/error502.html")
}

resource "azurerm_storage_blob" "blob_appgw_sd_admin_ui_error403" {
  name                   				= "error403.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_sd_admin_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_sd_admin_ui}/error403.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_sd_admin_ui}/error403.html")
}

resource "azurerm_storage_container" "container_appgw_sd_ui" {
  name                  				= "${var.prefix}sacontappgwsdui"
  storage_account_id  				= azurerm_storage_account.storage_appgw_errorpage.id
  container_access_type 				= "blob"
}

resource "azurerm_storage_blob" "blob_appgw_sd_ui_error502" {
  name                   				= "error502.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_sd_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_sd_ui}/error502.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_sd_ui}/error502.html")
}

resource "azurerm_storage_account" "storage_db_logs" {
  name                = "${var.prefix}sadblogs"
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
  provisioner "local-exec" {
    command = "az storage account update --name ${self.name} --resource-group ${self.resource_group_name} --key-exp-days 365"
    interpreter = ["pwsh", "-Command"]
  }
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

resource "azurerm_storage_container" "container_db_va_logs" {
  name                  = "${var.prefix}sadbvalogs"
  storage_account_id  = azurerm_storage_account.storage_db_logs.id
  container_access_type = "blob"
}

# Create Virtual Network
resource "azurerm_virtual_network" "vnet" {
  name                = "${var.prefix}-fh-vn-01"
  address_space       = var.vnet_address_space
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags
}

# Create Appgtway Subnet
resource "azurerm_subnet" "applicationgateway" {
  name                 = "${var.prefix}-fh-appgw-sn"
  address_prefixes         = var.ag_address_space
  resource_group_name = local.resource_group_name
  virtual_network_name =  azurerm_virtual_network.vnet.name
  private_endpoint_network_policies = "Disabled"
}

# Create vNET Integration Subnet
resource "azurerm_subnet" "vnetint" {
  name                 = "${var.prefix}-fh-vnetint-sn"
  address_prefixes         = var.vnetint_address_space
  resource_group_name = local.resource_group_name
  virtual_network_name =  azurerm_virtual_network.vnet.name
  private_endpoint_network_policies = "Disabled"
  delegation {
    name = "delegation"

    service_delegation {
      name    = "Microsoft.Web/serverFarms"
      actions = ["Microsoft.Network/virtualNetworks/subnets/join/action", "Microsoft.Network/virtualNetworks/subnets/prepareNetworkPolicies/action"]
    }
  }
  lifecycle {
    ignore_changes = [delegation]
  }
}

# Create Private Endpoint Subnet
resource "azurerm_subnet" "pvtendpoint" {
  name                 = "${var.prefix}-fh-pvtendpt-sn"
  address_prefixes         = var.pvtendpt_address_space
  resource_group_name = local.resource_group_name
  virtual_network_name =  azurerm_virtual_network.vnet.name
  private_endpoint_network_policies = "Disabled"
}

# Create SQL Server Subnet
resource "azurerm_subnet" "sqlserver" {
  name                 = "${var.prefix}-fh-sqlserver-sn"
  address_prefixes         = var.sql_server_address_space
  resource_group_name = local.resource_group_name
  virtual_network_name =  azurerm_virtual_network.vnet.name
  private_endpoint_network_policies = "Disabled"
}

# Email Group for alert monitoring
resource "azurerm_monitor_action_group" "email_grp" {
  name                  = "${var.prefix}-email-alert-grp"
  resource_group_name   = local.resource_group_name
  short_name            = "shrnm"
  email_receiver {
    email_address       = "${var.slack_support_channel_email}"
    name                = "FamilyHub"
  }
  tags = local.tags
}

# Create Defender Protection for App Services
# Note: We only want to manage this on the initial resources created in each subscription as this happens at subscription level
# hence the count block
# This ticket: https://dfedigital.atlassian.net.mcas.ms/browse/FHB-906 will handle removing this
resource "azurerm_security_center_subscription_pricing" "app_services" {
  tier = var.defender_app_services_tier
  resource_type = "AppServices"
}