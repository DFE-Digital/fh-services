subscription_id = "779425ba-87e7-47c3-9481-d15b47d37020"
prefix = "s181t02"
environment = "Test"

vnet_address_space = ["10.0.3.0/24"] # Range is 10.0.3.0 - 10.0.3.255
ag_address_space = ["10.0.3.0/26"] # Range is 10.0.3.0 - 10.0.3.63
vnetint_address_space = ["10.0.3.80/28"] # Range is 10.0.3.80 - 10.0.3.95
pvtendpt_address_space = ["10.0.3.96/28"] # Range is 10.0.3.96 - 10.0.3.111
sql_server_address_space = ["10.0.3.112/28"] # Range is 10.0.3.112 - 10.0.3.127

appgw_errorpage_path_referral_ui = "files/errorpages/referral_ui/test2"
appgw_errorpage_path_sd_admin_ui = "files/errorpages/sd_admin_ui/test2"
appgw_errorpage_path_sd_ui = "files/errorpages/sd_ui/test2"

service_principals = {
  reader_usr_group_object_id = "2c713f1b-2c2a-413c-879f-3b48a121bc19"
  delivery_team_user_group_object_id = "17c0fa92-3b66-4819-a08a-212778ddf7af"
  ado_enterprise_object_id = "1b550fb9-2030-42c4-999f-fadb0295f33b"
  github_enterprise_object_id = "78e77ad4-39c7-4ea7-8885-676da47f9476"
}

connect_domain = "test2.connect-families-to-support.education.gov.uk"
manage_domain = "test2.manage-family-support-services-and-accounts.education.gov.uk"
find_domain = "test2.find-support-for-your-family.education.gov.uk"

private_endpoint_ip_address = {
  referral_api = "10.0.3.100"
  referral_ui = "10.0.3.101"
  service_directory_api = "10.0.3.102"
  service_directory_ui = "10.0.3.103"
  service_directory_admin_ui = "10.0.3.104"
  notification_api = "10.0.3.105"
  idam_api = "10.0.3.107"
  report_api_ip = "10.0.3.108"
  open_referral_mock = "10.0.3.110"
  open_referral_func = "10.0.3.111"
  sql_server = "10.0.3.116"
  idam_maintenance_ui = "10.0.3.99"
}

pvt_endp_report_stg_api_ip_address = "10.0.3.109"
data_factory_exists = false