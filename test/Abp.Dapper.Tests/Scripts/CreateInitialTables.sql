 CREATE TABLE IF NOT EXISTS Products (
											Id INTEGER PRIMARY KEY
										,	Name varchar(100) 
										,	IsDeleted BOOLEAN
										,	DeleterUserId CHAR(36)
										,	DeletionTime DATETIME
										,	LastModificationTime DATETIME
										,	LastModifierUserId CHAR(36)
										,	CreationTime DATETIME
										,	CreatorUserId CHAR(36)
										,	TenantId INTEGER NULLABLE
										, Status BOOLEAN
									);

 CREATE TABLE IF NOT EXISTS ProductDetails (
											Id INTEGER PRIMARY KEY
										,	Gender varchar(100) 
										,	IsDeleted BOOLEAN
										,	DeleterUserId CHAR(36)
										,	DeletionTime DATETIME
										,	LastModificationTime DATETIME
										,	LastModifierUserId CHAR(36)
										,	CreationTime DATETIME
										,	CreatorUserId CHAR(36)
										,	TenantId INTEGER
									);

  CREATE TABLE IF NOT EXISTS Person (
											Id INTEGER PRIMARY KEY
										,	Name varchar(100) 
										,	TenantId INTEGER
									);

 CREATE TABLE IF NOT EXISTS Goods (
											Id INTEGER PRIMARY KEY
										,	Name varchar(100) 
										,	IsDeleted BOOLEAN
										,	DeleterUserId CHAR(36)
										,	DeletionTime DATETIME
										,	LastModificationTime DATETIME
										,	LastModifierUserId CHAR(36)
										,	CreationTime DATETIME
										,	CreatorUserId CHAR(36)
										,	ParentId INTEGER NULLABLE
										,	TenantId INTEGER
									);

 