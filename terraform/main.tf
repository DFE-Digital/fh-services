#Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "= 3.116.0"
    }
  }
}

provider "azurerm" {
  features {
    key_vault {
      purge_soft_deleted_keys_on_destroy = true
      recover_soft_deleted_keys = false
    }
  }
}

# This is overriden by the pipeline
terraform {
  backend "azurerm" {
    resource_group_name = "s181d01-familyhubs-terraform"
    storage_account_name = "s181d01safhterraform"
    container_name = "s181d01appserviceterraform"
    key = "s181d01appserviceterraform/s181d01appservicetf.tfstate"
  }
}

module "fhinfrastructurestack" {
  source = "./modules/fhinfrastructurestack"
  location = var.location
  prefix = var.prefix
  environment = var.environment
  vnet_address_space = var.vnet_address_space
  ag_address_space = var.ag_address_space
  vnetint_address_space = var.vnetint_address_space
  pvtendpt_address_space = var.pvtendpt_address_space
  sql_server_address_space = var.sql_server_address_space
  os_type = var.os_type
  sku_name = var.sku_name
  current_stack = var.current_stack
  dotnet_version_general = var.dotnet_version_general
  certificate_password = var.certificate_password
  service_principals = var.service_principals
  sql_server_user = var.sql_server_user
  sql_server_pwd = var.sql_server_pwd
  sql_server_reporting_user = var.sql_server_reporting_user
  sql_server_reporting_pwd = var.sql_server_reporting_pwd
  appgw_errorpage_path_referral_ui = var.appgw_errorpage_path_referral_ui
  appgw_errorpage_path_sd_admin_ui = var.appgw_errorpage_path_sd_admin_ui
  appgw_errorpage_path_sd_ui = var.appgw_errorpage_path_sd_ui
  ssl_cert_path_referral_ui = var.ssl_cert_path_referral_ui
  ssl_cert_path_sd_admin_ui = var.ssl_cert_path_sd_admin_ui
  ssl_cert_path_sd_ui = var.ssl_cert_path_sd_ui
  autoscale_rule_default_capacity = var.autoscale_rule_default_capacity
  autoscale_rule_minimum_capacity = var.autoscale_rule_minimum_capacity
  autoscale_rule_maximum_capacity = var.autoscale_rule_maximum_capacity
  email_notify = var.email_notify
  asp_netcore_environment = var.asp_netcore_environment
  defender_app_services_tier = var.defender_app_services_tier
  private_endpoint_ip_address = var.private_endpoint_ip_address
  connect_domain = var.connect_domain
  manage_domain = var.manage_domain
  find_domain = var.find_domain
  pvt_endp_report_stg_api_ip_address = var.pvt_endp_report_stg_api_ip_address
}