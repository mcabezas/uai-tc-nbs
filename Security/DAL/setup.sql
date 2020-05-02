IF OBJECT_ID('USER_PERMISSION', 'U') IS NOT NULL 
DROP TABLE IF EXISTS USER_PERMISSION;

IF OBJECT_ID('ROLE_ROLE', 'U') IS NOT NULL 
DROP TABLE IF EXISTS ROLE_ROLE;

IF OBJECT_ID('ROLE_PERMISSION', 'U') IS NOT NULL 
DROP TABLE IF EXISTS ROLE_PERMISSION;

IF OBJECT_ID('USER', 'U') IS NOT NULL 
DROP TABLE IF EXISTS [USER];

CREATE TABLE [USER] (
  id         int IDENTITY(1,1),
  email      TEXT NOT NULL,
  first_name TEXT,
  last_name  TEXT,
  password   TEXT NOT NULL,
  PRIMARY KEY (id)
)

IF OBJECT_ID('PERMISSION', 'U') IS NOT NULL 
DROP TABLE IF EXISTS PERMISSION;

CREATE TABLE PERMISSION (
  id          int  IDENTITY(1,1),
  description TEXT,
  PRIMARY KEY (id)
)

IF OBJECT_ID('ROLE', 'U') IS NOT NULL 
DROP TABLE IF EXISTS [ROLE];

CREATE TABLE [ROLE] (
  id          int  IDENTITY(1,1),
  description TEXT,
  PRIMARY KEY (id)
)

CREATE TABLE ROLE_ROLE (
  role_id  int,
  child_role_id int,
  PRIMARY KEY (role_id, child_role_id),
  FOREIGN KEY (role_id) REFERENCES [ROLE](id),
  FOREIGN KEY (child_role_id) REFERENCES [ROLE](id)
)

CREATE TABLE ROLE_PERMISSION (
  role_id       int,
  permission_id int,
  PRIMARY KEY (role_id, permission_id),
  FOREIGN KEY (role_id) REFERENCES [ROLE](id),
  FOREIGN KEY (permission_id) REFERENCES [PERMISSION](id)
)

CREATE TABLE USER_PERMISSION (
  user_id       int,
  permission_id int,
  PRIMARY KEY (user_id, permission_id),
  FOREIGN KEY (user_id) REFERENCES [USER](id),
  FOREIGN KEY (permission_id) REFERENCES [PERMISSION](id)
)