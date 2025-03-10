subscription_id = "56c128b8-5ca9-4328-bb24-a11c829f4af9"
prefix = "s181d01"
asp_netcore_environment = "Development"
environment = "Dev"

vnet_address_space = ["10.0.0.0/24"]
ag_address_space = ["10.0.0.0/26"]
vnetint_address_space = ["10.0.0.80/28"]
pvtendpt_address_space = ["10.0.0.96/28"]
sql_server_address_space = ["10.0.0.112/28"]

appgw_errorpage_path_referral_ui = "files/errorpages/referral_ui/dev"
appgw_errorpage_path_sd_admin_ui = "files/errorpages/sd_admin_ui/dev"
appgw_errorpage_path_sd_ui = "files/errorpages/sd_ui/dev"

service_principals = {
  reader_usr_group_object_id = "2c713f1b-2c2a-413c-879f-3b48a121bc19"
  delivery_team_user_group_object_id = "17c0fa92-3b66-4819-a08a-212778ddf7af"
  ado_enterprise_object_id = "30ed78a4-bd73-4ead-8f41-cb0bd17d4214"
  github_enterprise_object_id = "d134b2d5-cc70-4f68-8f7f-bf99890b49a1"
}

connect_domain = "dev.connect-families-to-support.education.gov.uk"
manage_domain = "dev.manage-family-support-services-and-accounts.education.gov.uk"
find_domain = "dev.find-support-for-your-family.education.gov.uk"

private_endpoint_ip_address = {
  referral_api = "10.0.0.100"
  referral_ui = "10.0.0.101"
  service_directory_api = "10.0.0.102"
  service_directory_ui = "10.0.0.103"
  service_directory_admin_ui = "10.0.0.104"
  notification_api = "10.0.0.105"
  idam_api = "10.0.0.107"
  report_api_ip = "10.0.0.108"
  open_referral_mock = "10.0.0.110"
  open_referral_func = "10.0.0.111"
  sql_server = "10.0.0.116"
  idam_maintenance_ui = "10.0.0.132"
}

autoscale_rule_default_capacity = "1"
autoscale_rule_minimum_capacity = "1"

pvt_endp_report_stg_api_ip_address = "10.0.0.109"
