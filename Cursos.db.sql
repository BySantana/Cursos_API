BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "AspNetRoles" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT,
	"NormalizedName"	TEXT,
	"ConcurrencyStamp"	TEXT,
	CONSTRAINT "PK_AspNetRoles" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "AspNetUsers" (
	"Id"	INTEGER NOT NULL,
	"PrimeiroNome"	TEXT,
	"UltimoNome"	TEXT,
	"UserName"	TEXT,
	"NormalizedUserName"	TEXT,
	"Email"	TEXT,
	"NormalizedEmail"	TEXT,
	"EmailConfirmed"	INTEGER NOT NULL,
	"PasswordHash"	TEXT,
	"SecurityStamp"	TEXT,
	"ConcurrencyStamp"	TEXT,
	"PhoneNumber"	TEXT,
	"PhoneNumberConfirmed"	INTEGER NOT NULL,
	"TwoFactorEnabled"	INTEGER NOT NULL,
	"LockoutEnd"	TEXT,
	"LockoutEnabled"	INTEGER NOT NULL,
	"AccessFailedCount"	INTEGER NOT NULL,
	CONSTRAINT "PK_AspNetUsers" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Categorias" (
	"CategoriaId"	INTEGER NOT NULL,
	"CategoriaNome"	TEXT,
	CONSTRAINT "PK_Categorias" PRIMARY KEY("CategoriaId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "AspNetRoleClaims" (
	"Id"	INTEGER NOT NULL,
	"RoleId"	INTEGER NOT NULL,
	"ClaimType"	TEXT,
	"ClaimValue"	TEXT,
	CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY("RoleId") REFERENCES "AspNetRoles"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserClaims" (
	"Id"	INTEGER NOT NULL,
	"UserId"	INTEGER NOT NULL,
	"ClaimType"	TEXT,
	"ClaimValue"	TEXT,
	CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserLogins" (
	"LoginProvider"	TEXT NOT NULL,
	"ProviderKey"	TEXT NOT NULL,
	"ProviderDisplayName"	TEXT,
	"UserId"	INTEGER NOT NULL,
	CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY("LoginProvider","ProviderKey"),
	CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserRoles" (
	"UserId"	INTEGER NOT NULL,
	"RoleId"	INTEGER NOT NULL,
	CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY("UserId","RoleId"),
	CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY("RoleId") REFERENCES "AspNetRoles"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserTokens" (
	"UserId"	INTEGER NOT NULL,
	"LoginProvider"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	"Value"	TEXT,
	CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY("UserId","LoginProvider","Name"),
	CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Logs" (
	"Id"	INTEGER NOT NULL,
	"CursoId"	INTEGER NOT NULL,
	"DataInclusao"	TEXT NOT NULL,
	"DataAtualizacao"	TEXT,
	"UserId"	INTEGER NOT NULL,
	"Status"	TEXT,
	CONSTRAINT "FK_Logs_Cursos_CursoId" FOREIGN KEY("CursoId") REFERENCES "Cursos"("CursoId") ON DELETE CASCADE,
	CONSTRAINT "FK_Logs_AspNetUsers_UserId" FOREIGN KEY("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE,
	CONSTRAINT "PK_Logs" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Cursos" (
	"CursoId"	INTEGER NOT NULL,
	"CategoriaId"	INTEGER NOT NULL,
	"DataInicio"	TEXT NOT NULL,
	"DataTermino"	TEXT NOT NULL,
	"Descricao"	TEXT,
	"QtdAlunos"	INTEGER NOT NULL,
	"Status"	INTEGER NOT NULL,
	"UserId"	INTEGER NOT NULL,
	CONSTRAINT "PK_Cursos" PRIMARY KEY("CursoId" AUTOINCREMENT),
	CONSTRAINT "FK_Cursos_AspNetUsers_UserId" FOREIGN KEY("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Cursos_Categorias_CategoriaId" FOREIGN KEY("CategoriaId") REFERENCES "Categorias"("CategoriaId") ON DELETE CASCADE
);
INSERT INTO "__EFMigrationsHistory" VALUES ('20220707133337_Inicial','5.0.17');
INSERT INTO "__EFMigrationsHistory" VALUES ('20220712122520_TituloRemovido','5.0.17');
INSERT INTO "__EFMigrationsHistory" VALUES ('20220714161717_Adicionado campo status','5.0.17');
INSERT INTO "AspNetRoles" VALUES (1,'admin','admin','admin');
INSERT INTO "AspNetUsers" VALUES (1,'admin','admin','admin','ADMIN','admin@gmail.com','ADMIN@GMAIL.COM',0,'AQAAAAEAACcQAAAAENRJEvncaJlLDqXHkVF9BQcCIABgJHr0b3rXd6Eg6uT65TXQTd1fPahX643KXzeEOg==','TBU35FLZHY5HQCUVOPZZGGRO5AKREVIG','d4c7664f-cd6a-4863-9bcf-17936af79d41',NULL,0,0,NULL,1,0);
INSERT INTO "AspNetUsers" VALUES (2,'Diego','Santana','diego','DIEGO','diego@gmail','DIEGO@GMAIL',0,'AQAAAAEAACcQAAAAEIyI+HMXaIwx+xdFz355OrQ2+RcfSj+frdwyT59zFAobZAAlE9ASuBmLvpfdmwls5g==','Z5K7XTVDUR7QM6RCIN7GHSRS42CRQY3B','4b1d2252-6776-42f2-ada4-c513a05e03a0',NULL,0,0,NULL,1,0);
INSERT INTO "AspNetUsers" VALUES (4,'Marcos','Vinicius','marcos','MARCOS','marcos@gmail.com','MARCOS@GMAIL.COM',0,'AQAAAAEAACcQAAAAEORUb7gxeAgkYV9st5+JvWXYPHvoEW37xq0pfsES4Icn5MpNRelR45ZejhWq78Um0g==','IZMBWA5ACI4L2RTLKINMOXIDOPDFAJBY','11404486-2a37-4680-abeb-231582cb166f',NULL,0,0,NULL,1,0);
INSERT INTO "AspNetUsers" VALUES (5,'Lucas','Carvalho','lucas','LUCAS','lucas@gmail','LUCAS@GMAIL',0,'AQAAAAEAACcQAAAAENgmhLzRFWWLO2ciU1OvE0/vkWf/gMLYCao0UnIo1l2Z5F6g+lHNHNpYiexT7Wrx1g==','CLTAGHI55BUIBTDHQQZOHJIK7ZK5VUGA','cffc1e5b-836a-4225-9b0a-ee58ce87eadf',NULL,0,0,NULL,1,0);
INSERT INTO "Categorias" VALUES (1,'Multiplataforma');
INSERT INTO "Categorias" VALUES (2,'Banco de dados');
INSERT INTO "Categorias" VALUES (3,'Metodologia');
INSERT INTO "Categorias" VALUES (4,'Comportamento');
INSERT INTO "Categorias" VALUES (5,'Comunicação');
INSERT INTO "AspNetUserRoles" VALUES (1,1);
INSERT INTO "Logs" VALUES (23,1,'2022-07-12 09:37:28.4054398',NULL,2,'Curso criado');
INSERT INTO "Logs" VALUES (24,2,'2022-07-12 10:37:19.5395293','2022-07-13 18:11:09.4516062',4,'Curso atualizado');
INSERT INTO "Logs" VALUES (25,3,'2022-07-12 11:38:05.6329991','2022-07-14 12:41:46.4295749',5,'Curso atualizado');
INSERT INTO "Logs" VALUES (26,4,'2022-07-12 18:20:03.7684142','2022-07-14 13:25:56.1634402',2,'Curso excluído');
INSERT INTO "Logs" VALUES (27,5,'2022-07-13 10:43:22.0579022',NULL,5,'Curso criado');
INSERT INTO "Logs" VALUES (33,11,'2022-07-14 12:28:42.3265873',NULL,4,'Curso criado');
INSERT INTO "Logs" VALUES (34,12,'2022-07-14 13:22:11.1289559','2022-07-14 13:25:47.4975382',2,'Curso excluído');
INSERT INTO "Logs" VALUES (35,13,'2022-07-14 13:33:51.7453434','2022-07-14 14:54:17.0282793',2,'Curso atualizado');
INSERT INTO "Logs" VALUES (36,14,'2022-07-14 13:43:37.0864472','2022-07-14 13:45:05.5151775',5,'Curso atualizado');
INSERT INTO "Cursos" VALUES (1,1,'2022-07-13 03:00:00','2022-07-15 03:00:00','Asp NET',25,1,2);
INSERT INTO "Cursos" VALUES (2,2,'2022-07-18 03:00:00','2022-07-20 03:00:00','SqlServer',40,1,4);
INSERT INTO "Cursos" VALUES (3,3,'2022-08-03 03:00:00','2022-08-09 03:00:00','Scrum',32,1,5);
INSERT INTO "Cursos" VALUES (4,3,'2022-07-22 03:00:00','2022-07-27 03:00:00','Java',30,0,2);
INSERT INTO "Cursos" VALUES (5,2,'2022-07-30 03:00:00','2022-08-02 03:00:00','SqLite',36,1,5);
INSERT INTO "Cursos" VALUES (11,5,'2022-08-22 03:00:00','2022-08-25 03:00:00','Angular',15,1,4);
INSERT INTO "Cursos" VALUES (12,3,'2022-08-29 03:00:00','2022-08-31 03:00:00','teste1',15,0,2);
INSERT INTO "Cursos" VALUES (13,4,'2022-08-28 03:00:00','2022-08-31 03:00:00','JavaScript',36,1,2);
INSERT INTO "Cursos" VALUES (14,2,'2022-08-10 03:00:00','2022-08-13 03:00:00','Excel',12,1,5);
CREATE INDEX IF NOT EXISTS "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" (
	"RoleId"
);
CREATE UNIQUE INDEX IF NOT EXISTS "RoleNameIndex" ON "AspNetRoles" (
	"NormalizedName"
);
CREATE INDEX IF NOT EXISTS "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" (
	"UserId"
);
CREATE INDEX IF NOT EXISTS "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" (
	"UserId"
);
CREATE INDEX IF NOT EXISTS "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" (
	"RoleId"
);
CREATE INDEX IF NOT EXISTS "EmailIndex" ON "AspNetUsers" (
	"NormalizedEmail"
);
CREATE UNIQUE INDEX IF NOT EXISTS "UserNameIndex" ON "AspNetUsers" (
	"NormalizedUserName"
);
CREATE INDEX IF NOT EXISTS "IX_Logs_CursoId" ON "Logs" (
	"CursoId"
);
CREATE INDEX IF NOT EXISTS "IX_Logs_UserId" ON "Logs" (
	"UserId"
);
CREATE INDEX IF NOT EXISTS "IX_Cursos_CategoriaId" ON "Cursos" (
	"CategoriaId"
);
CREATE INDEX IF NOT EXISTS "IX_Cursos_UserId" ON "Cursos" (
	"UserId"
);
COMMIT;
