﻿ CREATE TABLE IF NOT EXISTS Products (
											Id INTEGER PRIMARY KEY
										,	Name varchar(100) 
										,	IsDeleted BOOLEAN
										,	DeleterUserId BIGINT
										,	DeletionTime DATETIME
										,	LastModificationTime DATETIME
										,	LastModifierUserId BIGINT
										,	CreationTime DATETIME
										,	CreatorUserId BIGINT
										,	TenantId BIGINT NULLABLE
										, Status BOOLEAN
									);

 CREATE TABLE IF NOT EXISTS ProductDetails (
											Id INTEGER PRIMARY KEY
										,	Gender varchar(100) 
										,	IsDeleted BOOLEAN
										,	DeleterUserId BIGINT
										,	DeletionTime DATETIME
										,	LastModificationTime DATETIME
										,	LastModifierUserId BIGINT
										,	CreationTime DATETIME
										,	CreatorUserId BIGINT
										,	TenantId BIGINT
									);

  CREATE TABLE IF NOT EXISTS Person (
											Id INTEGER PRIMARY KEY
										,	Name varchar(100) 
										,	TenantId BIGINT
									);

 CREATE TABLE IF NOT EXISTS Goods (
											Id INTEGER PRIMARY KEY
										,	Name varchar(100) 
										,	IsDeleted BOOLEAN
										,	DeleterUserId BIGINT
										,	DeletionTime DATETIME
										,	LastModificationTime DATETIME
										,	LastModifierUserId BIGINT
										,	CreationTime DATETIME
										,	CreatorUserId BIGINT
										,	ParentId INTEGER NULLABLE
										,	TenantId BIGINT
									);

 