BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Equipo" (
	"Id"	TEXT,
	"Nombre"	TEXT,
	"AñoFundacion"	TEXT,
	"Logo"	TEXT,
	"Estadio"	TEXT,
	"Ciudad"	TEXT,
	PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Usuarios" (
	"Usuario"	TEXT NOT NULL,
	"Password"	TEXT NOT NULL,
	"Permisos"	TEXT NOT NULL DEFAULT 'False',
	"EquipoFav"	TEXT,
	"Correo"	TEXT,
	PRIMARY KEY("Usuario")
);
INSERT INTO "Equipo" ("Id","Nombre","AñoFundacion","Logo","Estadio","Ciudad") VALUES ('529','Barcelona','1899','https://media-1.api-sports.io/football/teams/529.png','Spotify Camp Nou','Barcelona'),
 ('530','Atletico Madrid','1903','https://media-1.api-sports.io/football/teams/530.png','Estádio Cívitas Metropolitano','Madrid'),
 ('531','Athletic Club','1898','https://media-3.api-sports.io/football/teams/531.png','San Mamés Barria','Bilbao'),
 ('532','Valencia','1919','https://media-2.api-sports.io/football/teams/532.png','Estadio de Mestalla','Valencia'),
 ('533','Villarreal','1923','https://media-2.api-sports.io/football/teams/533.png','Estadio de la Cerámica','Villarreal'),
 ('536','Sevilla','1890','https://media-3.api-sports.io/football/teams/536.png','Estadio Ramón Sánchez Pizjuán','Sevilla'),
 ('538','Celta Vigo','1923','https://media-2.api-sports.io/football/teams/538.png','Abanca-Balaídos','Vigo'),
 ('540','Espanyol','1900','https://media-1.api-sports.io/football/teams/540.png','RCDE Stadium','Cornella de Llobregat'),
 ('541','Real Madrid','1902','https://media-3.api-sports.io/football/teams/541.png','Estadio Santiago Bernabéu','Madrid'),
 ('543','Real Betis','1907','https://media-1.api-sports.io/football/teams/543.png','Estadio Benito Villamarín','Sevilla'),
 ('546','Getafe','1983','https://media-2.api-sports.io/football/teams/546.png','Coliseum Alfonso Pérez','Getafe'),
 ('547','Girona','1930','https://media-2.api-sports.io/football/teams/547.png','Estadi Municipal de Montilivi','Girona'),
 ('548','Real Sociedad','1909','https://media-3.api-sports.io/football/teams/548.png','Reale Arena','Donostia-San Sebastián'),
 ('720','Valladolid','1928','https://media-3.api-sports.io/football/teams/720.png','Estadio Municipal José Zorrilla','Valladolid'),
 ('723','Almeria','1989','https://media-3.api-sports.io/football/teams/723.png','Power Horse Stadium – Estadio de los Juegos Mediterráneos','Almería'),
 ('724','Cadiz','1910','https://media-2.api-sports.io/football/teams/724.png','Estadio Nuevo Mirandilla','Cádiz'),
 ('727','Osasuna','1920','https://media-3.api-sports.io/football/teams/727.png','Estadio El Sadar','Iruñea'),
 ('728','Rayo Vallecano','1924','https://media-2.api-sports.io/football/teams/728.png','Estadio de Vallecas','Madrid'),
 ('797','Elche','1923','https://media-2.api-sports.io/football/teams/797.png','Estadio Manuel Martínez Valero','Elche'),
 ('798','Mallorca','1916','https://media-3.api-sports.io/football/teams/798.png','Visit Mallorca Estadi','Palma de Mallorca');
INSERT INTO "Usuarios" ("Usuario","Password","Permisos","EquipoFav","Correo") VALUES ('Rodrigc153','231gH','false','541','md.nonpersonal@gmail.com'),
 ('pardo_mario10','Yuraku18','false','543','md.nonpersonal@gmail.com'),
 ('admin','admin','True','541','md.nonpersonal@gmail.com'),
 ('Probando','EF8575C90E44AE42FD1BC7BAA41F3CEF20F73C4EEE921EFCC63D4EA70485DA8D','True','536','md.nonpersonal@gmail.com'),
 ('Probando5','451623993DF13C3A846272E8001EC73441B734DC0554F6EA25BEAD75E27D4EA8','false','548','md.nonpersonal@gmail.com'),
 ('Profesor2023','E1C7D4B8370596176C2CD45ABBF333AFCCD671E63FE93EB660EE3E741104D626','True','','md.nonpersonal@gmail.com'),
 ('Cristian','DBF7AD52D2EE09081ED566ADBEA5D3E57E186A9ABA99091E355CCA8310278D15','false','797','md.nonpersonal@gmail.com'),
 ('Versus2022','131E8AFDB12FB6669EA5257ED49C280DB07627932C4FF1BDDFF2ECDF83B4FF21','True','',''),
 ('Pablo2023','2AE62D384AE7096B7164E6D687A5B604E5615225FAB36E36B4FA9AD39F58D079','True','','md.nonpersonal@gmail.com'),
 ('RetroSlasher','03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4','True','541','md.nonpersonal@gmail.com'),
 ('PepeTheFrog20','2EA2862D2275303F5396C1E2B416A023933F0402A10C24B7A3125BD6B49EFCD2','false','532','md.nonpersonal@gmail.com');
COMMIT;
