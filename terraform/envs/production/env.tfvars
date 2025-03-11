subscription_id = "ee94c326-6d5d-4cc3-b439-4e39dfde6ebc"
prefix = "s181p01"
environment = "Prod"
defender_app_services_tier = "Standard"
autoscale_rule_default_capacity = 4
autoscale_rule_minimum_capacity = 4

vnet_address_space = ["10.0.2.0/24"]
ag_address_space = ["10.0.2.0/26"]
vnetint_address_space = ["10.0.2.80/28"]
pvtendpt_address_space = ["10.0.2.96/28"]
sql_server_address_space = ["10.0.2.112/28"]

appgw_errorpage_path_referral_ui = "files/errorpages/referral_ui/prod"
appgw_errorpage_path_sd_admin_ui = "files/errorpages/sd_admin_ui/prod"
appgw_errorpage_path_sd_ui = "files/errorpages/sd_ui/prod"

service_principals = {
  reader_usr_group_object_id = "2c713f1b-2c2a-413c-879f-3b48a121bc19"
  delivery_team_user_group_object_id = "17c0fa92-3b66-4819-a08a-212778ddf7af"
  ado_enterprise_object_id = "3c30b876-66b4-4aa9-848b-e4657f0c6973"
  github_enterprise_object_id = "3fbf6a57-8437-47c6-b340-736cbca576c0"
}

connect_domain = "connect-families-to-support.education.gov.uk"
manage_domain = "manage-family-support-services-and-accounts.education.gov.uk"
find_domain = "find-support-for-your-family.education.gov.uk"

log_retention_in_days = 30

private_endpoint_ip_address = {
  referral_api = "10.0.2.100"
  referral_ui = "10.0.2.101"
  service_directory_api = "10.0.2.102"
  service_directory_ui = "10.0.2.103"
  service_directory_admin_ui = "10.0.2.104"
  notification_api = "10.0.2.105"
  idam_api = "10.0.2.107"
  report_api_ip = "10.0.2.108"
  open_referral_mock = "10.0.2.110"
  open_referral_func = "10.0.2.111"
  sql_server = "10.0.2.116"
  idam_maintenance_ui = "10.0.2.132"
}

pvt_endp_report_stg_api_ip_address = "10.0.2.109"
