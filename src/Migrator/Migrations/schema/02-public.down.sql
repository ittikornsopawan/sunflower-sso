DROP INDEX IF EXISTS public.idx_t_personal_info_first_name_middle_name_last_name;
DROP INDEX IF EXISTS public.idx_t_files_file_name_file_extension;
DROP INDEX IF EXISTS public.idx_t_contacts_channel_contact;
DROP INDEX IF EXISTS public.idx_t_addresses_country_code_state_city;
DROP INDEX IF EXISTS public.idx_t_addresses_geofence_area;
DROP INDEX IF EXISTS public.idx_m_error_handlers_code_language;
DROP INDEX IF EXISTS public.idx_m_parameters_key;

DROP TABLE IF EXISTS public.t_personal_contacts CASCADE;
DROP TABLE IF EXISTS public.t_personal_addresses CASCADE;
DROP TABLE IF EXISTS public.t_personal_info CASCADE;
DROP TABLE IF EXISTS public.t_files CASCADE;
DROP TABLE IF EXISTS public.t_contacts CASCADE;
DROP TABLE IF EXISTS public.t_addresses CASCADE;
DROP TABLE IF EXISTS public.m_error_handlers CASCADE;
DROP TABLE IF EXISTS public.m_parameters CASCADE;