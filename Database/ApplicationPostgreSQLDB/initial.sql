DO $$ 
DECLARE 
    AppCode VARCHAR(10) := 'MES';
    CreateDate TIMESTAMP := '2025-02-02';
BEGIN
    INSERT INTO "tb_Application" VALUES (AppCode, 'RUBIX-X Framework', true, CreateDate, 1);
    INSERT INTO "tb_Permission" VALUES ('OPN', 'Open', 1, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_Permission" VALUES ('ADD', 'Add', 2, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_Permission" VALUES ('EDT', 'Edit', 3, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_Permission" VALUES ('DEL', 'Delete', 4, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_PermissionName" VALUES ('OPN', 'en-US', 'Open');
    INSERT INTO "tb_PermissionName" VALUES ('ADD', 'en-US', 'Add');
    INSERT INTO "tb_PermissionName" VALUES ('EDT', 'en-US', 'Edit');
    INSERT INTO "tb_PermissionName" VALUES ('DEL', 'en-US', 'Delete');
    INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS020', 'home', '/s/home', 1, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS030', 'person_add', '/s/auth/usr/s', 2, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS031', 'person_add', '/s/auth/usr/d', 3, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS040', 'group_add', '/s/auth/grp/s', 4, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS041', 'group_add', '/s/auth/grp/d', 5, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS050', 'folder_managed', '/s/auth/p/s', 6, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS051', 'folder_managed', '/s/auth/p/d', 7, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS060', 'web_asset', '/s/auth/s/s', 8, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS061', 'web_asset', '/s/auth/s/d', 9, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS070', 'list', '/s/auth/m/s', 10, true, CreateDate, 1, CreateDate, 1);
	INSERT INTO "tb_Screen" VALUES (AppCode, 'SSS071', 'list', '/s/auth/m/d', 11, true, CreateDate, 1, CreateDate, 1);
    -- Add more INSERT statements for tb_Screen as needed
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS020', 'en-US', 'Dashboard');
	INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS030', 'en-US', 'User Profile');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS031', 'en-US', 'User Profile Detail');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS040', 'en-US', 'User Group');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS041', 'en-US', 'User Group Detail');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS050', 'en-US', 'Permission');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS051', 'en-US', 'Permission Detail');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS060', 'en-US', 'Screen');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS061', 'en-US', 'Screen Detail');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS070', 'en-US', 'Menu');
    INSERT INTO "tb_ScreenName" VALUES (AppCode, 'SSS071', 'en-US', 'Menu Detail');
    -- Add more INSERT statements for tb_ScreenName as needed
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS031', 'ADD');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS041', 'ADD');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS051', 'ADD');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS061', 'ADD');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS071', 'ADD');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS031', 'DEL');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS041', 'DEL');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS051', 'DEL');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS061', 'DEL');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS071', 'DEL');
   
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS031', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS041', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS050', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS051', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS060', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS061', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS070', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS071', 'EDT');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS020', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS030', 'OPN');
   
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS031', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS040', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS041', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS050', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS051', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS060', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS061', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS070', 'OPN');
    INSERT INTO "tb_ScreenPermission" VALUES (AppCode, 'SSS071', 'OPN');

    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 1, 'G', NULL, 1, 'home', NULL, NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 2, 'I', 1, 1, NULL, 'SSS020', NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 3, 'G', NULL, 2, 'manage_accounts', NULL, NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 4, 'I', 3, 2, NULL, 'SSS030', NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 5, 'I', 3, 1, NULL, 'SSS040', NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 6, 'G', 3, 3, 'settings', NULL, NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 7, 'I', 6, 1, NULL, 'SSS050', NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 8, 'I', 6, 2, NULL, 'SSS060', NULL, true, CreateDate, 1, CreateDate, 1);
    INSERT INTO "tb_MenuSetting" VALUES (AppCode, 9, 'I', 6, 3, NULL, 'SSS070', NULL, true, CreateDate, 1, CreateDate, 1);
   
    INSERT INTO "tb_MenuName" VALUES (AppCode, 1, 'en-US', 'Home');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 2, 'en-US', 'Dashboard');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 3, 'en-US', 'Administration');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 4, 'en-US', 'User Profile');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 5, 'en-US', 'User Group');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 6, 'en-US', 'Setting');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 7, 'en-US', 'Permission');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 8, 'en-US', 'Screen');
    INSERT INTO "tb_MenuName" VALUES (AppCode, 9, 'en-US', 'Menu');
END $$;

