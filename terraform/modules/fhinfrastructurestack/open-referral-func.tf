
module "open_referral_storage_account" {
  source = "../storage_account"
  resource_group_name = local.resource_group_name
  location = var.location
  storage_account_name = "${var.prefix}orfunc"
  tags = local.tags
}

resource "azurerm_windows_function_app" "open_referral_function_app" {
  name = "${var.prefix}-fa-fh-open-referral"
  location = var.location
  resource_group_name = local.resource_group_name
  service_plan_id = azurerm_service_plan.apps_plan.id
  storage_account_name = module.open_referral_storage_account.storage_account_name
  storage_account_access_key = module.open_referral_storage_account.storage_account_primary_access_key
  app_settings = {
    APPLICATIONINSIGHTS_CONNECTION_STRING = azurerm_application_insights.app_insights.connection_string
    "AppConfiguration:KeyVaultIdentifier" = "${var.prefix}-kv-fh-admin"
    "AppConfiguration:KeyVaultPrefix" = "OPEN-REFERRAL-FUNC"
    "ServiceDirectoryConnection" = local.service_directory_db_connection
  }
  identity {
    type = "SystemAssigned"
  }
  site_config {
    ftps_state = "Disabled"
    use_32_bit_worker = false
    minimum_tls_version = "1.2"
    always_on = true
    application_stack {
      dotnet_version = var.dotnet_version_general
      use_dotnet_isolated_runtime = true
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
    cors {
      allowed_origins = ["https://portal.azure.com"]
    }
  }
  lifecycle {
    ignore_changes = [virtual_network_subnet_id]
  }
  tags = local.tags
}

resource "azurerm_app_service_virtual_network_swift_connection" "open_referral_func_vnet_swift_connection" {
  app_service_id = azurerm_windows_function_app.open_referral_function_app.id
  subnet_id = azurerm_subnet.vnetint.id
}

resource "azurerm_private_dns_a_record" "open_referral_mock_api_dns_a_record" {
  name = azurerm_windows_function_app.open_referral_function_app.name
  zone_name = azurerm_private_dns_zone.sqlserver.name
  resource_group_name = local.resource_group_name
  ttl = 10
  records = [var.private_endpoint_ip_address.open_referral_func]
  tags = local.tags
  lifecycle {
    ignore_changes = [tags]
  }
}