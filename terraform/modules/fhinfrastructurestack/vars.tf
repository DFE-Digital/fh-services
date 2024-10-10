variable "location" {
  type = string
  description = "The Azure region the services are deployed in."
  default = "westeurope"
}

variable "prefix" {
  type = string
  description = "The resource prefix such as s181t02."
}

variable "os_type" {
  type = string
  description = "The service app operating system."
  default = "Windows"
}

variable "sku_name" {
  type = string
  description = "The service plan SKU."
  default = "S3"
}

variable "current_stack" {
  type = string
  description = "The service code stack e.g. dotnet."
  default = "dotnet"
}

variable "dotnet_version_general" {
  type = string
  description = "Dotnet version."
  default = "v8.0"
}

variable "asp_netcore_environment" {
  type = string
  description = "The ASPNETCORE_ENVIRONMENT variable value."
  default = "Release"
}

variable "environment" {
  type = string
  description = "Environment tag."
}

variable "certificate_password" {
  type = string
  description = "SSL certificate password."
}

variable "vnet_address_space" {
  type = list(string)
  description = "VNET address space."
}
variable "ag_address_space" {
  type = list(string)
  description = "VNET app gateway address space."
}
variable "vnetint_address_space" {
  type = list(string)
  description = "VNET integration address space."
}
variable "pvtendpt_address_space" {
  type = list(string)
  description = "VNET private endpoint address space."
}
variable "sql_server_address_space" {
  type = list(string)
  description = "VNET SQL Server address space."
}

variable "sql_server_user" {
  type = string
  description = "The sql server SA username."
}

variable "sql_server_pwd" {
  type = string
  description = "The sql server SA password."
  sensitive = true
}

variable "sql_server_reporting_user" {
  type = string
  description = "The reporting sql server SA username."
}

variable "sql_server_reporting_pwd" {
  type = string
  description = "The reporting sql server SA password."
  sensitive = true
}

variable "appgw_errorpage_path_referral_ui" {
  type = string
  description = "Path to app gateway error page for the Connect UI."
}

variable "appgw_errorpage_path_sd_admin_ui" {
  type = string
  description = "Path to app gateway error page for the Manage UI."
}

variable "appgw_errorpage_path_sd_ui" {
  type = string
  description = "Path to app gateway error page for the Find UI."
}

variable "ssl_cert_path_referral_ui" {
  type = string
  description = "SSL certificate PFX base64 contents."
  sensitive = true
}

variable "ssl_cert_path_sd_admin_ui" {
  type = string
  description = "SSL certificate PFX base64 contents."
  sensitive = true
}

variable "ssl_cert_path_sd_ui" {
  type = string
  description = "SSL certificate PFX base64 contents."
  sensitive = true
}

variable "autoscale_rule_default_capacity" {
  type = number
  description = "The default app service capacity."
  default = 2
}

variable "autoscale_rule_minimum_capacity" {
  type = number
  description = "The minimum app service capacity."
  default = 2
}

variable "autoscale_rule_maximum_capacity" {
  type = number
  description = "The maximum app service capacity."
  default = 10
}

variable "email_notify" {
  type = string
  description = "Email to send alert notifications to."
  default = "growingupwell.lower@education.gov.uk"
}

variable "defender_app_services_tier" {
  type = string
  description = "Defender tier for app services"
  default = "Free"
}

variable "connect_domain" {
  type = string
  description = "Domain name for connect."
}

variable "manage_domain" {
  type = string
  description = "Domain name for manage."
}

variable "find_domain" {
  type = string
  description = "Domain name for find."
}

# Report Staging API Application - Variable Declaration - fh_report_stg_api

# Private End Point - IP Address
variable  "pvt_endp_report_stg_api_ip_address" {
  type = string
  description = "Staging IP address - to be deleted."
}

variable "private_endpoint_ip_address" {
  type = object({
    referral_api = string
    referral_ui = string
    service_directory_api = string
    service_directory_ui = string
    service_directory_admin_ui = string
    notification_api = string
    referral_dashboard_ui = string
    idam_api = string
    report_api_ip = string
    open_referral_mock = string
    open_referral_func = string
    sql_server = string
    idam_maintenance_ui = string
  })
  description = "Private endpoint IP addresses for the APIs, UIs, Functions and SQL Server."
}

variable "service_principals" {
  type = object({
    reader_usr_group_object_id = string # s181-growingupwell-Reader USR
    delivery_team_user_group_object_id = string # s181-growingupwell-Delivery Team USR
    referral_data_encryption_enterprise_object_id = string # s181d01-referral-data-encryption-v2 (Contributor)
    idam_data_encryption_enterprise_object_id = string # s181d01-idam-data-encryption (Contributor)
    notification_data_encryption_enterprise_object_id = string # s181d01-notification-data-encryption (Contributor)
    ado_enterprise_object_id = string # Allows ADO to deploy resources
    github_enterprise_object_id = string # Allows GitHub to deploy into the resource group
  })
  description = "Group and enterprise object Ids for service principals."
}