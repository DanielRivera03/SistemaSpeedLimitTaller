/**
    @@@@  @@@@  @@@@  @@@@  @@@@  @     @@@@  @@    @@  @@@@  @@@@@@    @@@@  @@@@@  @     @     @@@@  @@@@@@
    @@    @  @  @     @     @  @  @      @@   @@ @@ @@   @@    @@        @@   @@ @@  @     @     @     @@ @@@
    @@@@  @@@@  @@@@  @@@@  @  @  @      @@   @@    @@   @@    @@        @@   @@@@@  @     @     @@@@  @@@@@@
      @@  @@    @     @     @  @  @      @@   @@    @@   @@    @@        @@   @@ @@  @     @     @     @@  @@
    @@@@  @@    @@@@  @@@@  @@@@  @@@@  @@@@  @@    @@  @@@@   @@        @@   @@ @@  @@@@  @@@@  @@@@  @@   @@
**/



/**********************************************************************
 **********************************************************************
 **********************************************************************
 * AUTOR DEL SISTEMA: DANIEL RIVERA                                   *
 **********************************************************************
 * AUTORES BASE DE DATOS:											  *
 * DANIEL RIVERA, LUIS ANAYA Y BRYAN ROGEL							  *
 **********************************************************************
 * SISTEMA DE GESTION PARA CADENA REPUESTOS AUTOMOTRICES Y TALLER     *
 * SPEEDLIMIT TALLER S.A DE C.V.                                      *
 * © COPYRIGHT 2019 RESERVADOS TODOS LOS DERECHOS.                    *
 * VERSIÓN MEJORADA CON INTERFAZ GRÁFICA --> [2.0].                   *
 * SISTEMA TOTALMENTE DESARROLLADO DE CERO.                           *
 * PROYECTO LIBERADO Y COMPARTIDO PARA FINES EDUCATIVOS.              *
 **********************************************************************
 * PARA UNA CORRECTA VISUALIZACIÓN, LE RECOMENDAMOS UNA RESOLUCIÓN    *
 *                MAYOR O IGUAL A 1280 X 768                          *
 **********************************************************************
 * VERSIÓN ANTERIOR [1.0] --> DESARROLLADA EN C++                     *                
 * https://github.com/DanielRivera03/Sistema-Venta-Repuestos          *
***********************************************************************
***********************************************************************
***********************************************************************/

-- -> SENTENCIAS GENERALES {CREACION Y USO DE BASE DE DATOS}
GO
create database speedlimitdb
GO
use speedlimitdb


/*******************************************************************************************************************************************
											CREACION DE TABLAS GENERALES DEL SISTEMA
********************************************************************************************************************************************/


-- -> TABLA USUARIOS {LOGIN}
GO
create table [dbo].[usuario](
	[Id_usuario] int identity PRIMARY KEY NOT NULL,
	[Nombre] varchar(50) NOT NULL,
	[Usuario] varchar(50) UNIQUE NOT NULL,
	[Password] varchar(50) NOT NULL,
	[Tipo_usuario] varchar(50) NOT NULL,
);

-- -> TABLA EMPLEADOS
GO
create table [dbo].[Empleados](
	[Id_empleado] int identity PRIMARY KEY NOT NULL,
	[Codigo_empleado] varchar(100) UNIQUE NOT NULL,
	[Nombre_empleado] varchar (100) NOT NULL,
	[Apellido_empleado] varchar(100) NOT NULL,
	[Genero_empleado] CHAR(1) NOT NULL,
	[Email_empleado] NVARCHAR(100),
	[Contratacion_Empleado] date NOT NULL,
	[Dui_empleado] varchar(10) NOT NULL,
	[Nit_empleado] varchar(17) NOT NULL,
	[Salario_empleado] decimal(5,2) NOT NULL
);


-- -> TABLA PROVEEDORES DE PRODUCTOS
GO
create table [dbo].[ProveedoresProdutos](
	[ID_proveedor] varchar(100) PRIMARY KEY NOT NULL,
	[Nombre_proveedor] varchar(100) NOT NULL,
	[Sector_Comercial] VARCHAR(50) NOT NULL,
	[Direccion] VARCHAR(100) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Email] NVARCHAR(100) NOT NULL,
);

-- -> TABLA CATEGORIAS DE PRODUCTOS
GO
create table [dbo].[CategoriaProductos](
	[ID_categoria] INT IDENTITY PRIMARY KEY NOT NULL,
	[Nombre_Categoria] varchar(50) NOT NULL,
	[Descripcion_Categoria] varchar(400) NOT NULL,
);


-- -> TABLA PRODUCTOS SPEEDLIMIT TALLER S.A DE C.V
GO
create table [dbo].[Productos](
	[ID_producto] int identity PRIMARY KEY NOT NULL,
	[Cod_producto] VARCHAR(100) UNIQUE NOT NULL,
	[Nombre] varchar(75) NOT NULL,
	[Marca] varchar(75) NOT NULL,
	[Modelo] varchar(75) NOT NULL,
	[Precio] decimal(5,2) NOT NULL,
	[Nombre_proveedor] varchar(100) NOT NULL,
	[Nombre_Categoria] varchar(50) NOT NULL
);

-- -> TABLA CONTROL DE INVENTARIOS
GO
create table [dbo].[ControlInventarios](
	[ID_inventario] int identity PRIMARY KEY NOT NULL,
	[Numeros_Unidades_Stock] int NOT NULL,
	[Periodo_Garantia] int NOT NULL,
	[ID_producto] int UNIQUE NOT NULL FOREIGN KEY REFERENCES Productos,
	[Cod_producto] VARCHAR(100) UNIQUE NOT NULL,
	[Nombre] varchar(75) NOT NULL,
	[Marca] varchar(75) NOT NULL,
	[Modelo] varchar(75) NOT NULL,
	[Precio] decimal(5,2) NOT NULL,
	[ID_proveedor] varchar(100) NULL FOREIGN KEY REFERENCES ProveedoresProdutos,
	[Nombre_proveedor] varchar(100) NOT NULL,
	[ID_categoria] int NULL FOREIGN KEY REFERENCES CategoriaProductos,
	[Nombre_Categoria] varchar(50) NOT NULL
);

-- -> TABLA TIENDAS SPEEDLIMIT
GO
create table [dbo].[TiendasSpeedLimit](
	[ID_tienda] int identity PRIMARY KEY NOT NULL,
	[Codigo_tienda] varchar(100) UNIQUE NOT NULL,
	[Direccion_tienda] varchar(200) NOT NULL,
	[Tefefono_tienda] varchar(9) NOT NULL,
);

-- -> TABLA ENCARGADO TIENDAS SPEEDLIMIT
GO
create table [dbo].[EncargadosTiendas](
	[ID_encargado] int identity PRIMARY KEY NOT NULL,
	[Id_empleado] int FOREIGN KEY REFERENCES Empleados NULL,
	[Nombre_empleado] varchar (100) NOT NULL,
	[Apellido_empleado] varchar(100) NOT NULL,
	[ID_tienda] int FOREIGN KEY REFERENCES TiendasSpeedLimit,
	[Codigo_tienda] varchar(100) NOT NULL,
	[Direccion_tienda] varchar(200) NOT NULL,
	[Tefefono_tienda] varchar(9) NOT NULL,
);


/*******************************************************************************************************************************************
											 FIN CREACION DE TABLAS GENERALES DEL SISTEMA
*******************************************************************************************************************************************/


/*******************************************************************************************************************************************
											INSERTANDO REGISTROS A LAS TABLAS DEL SISTEMA
********************************************************************************************************************************************/

-- -> REGISTROS PARA TABLA USUARIOS {LOGIN DEL SISTEMA}
GO
insert into [usuario]
values('Daniel Rivera','daniel.rivera','12345','Admin'),		
	  ('Omar Guzman','omar.guzman','lorem.dolor','Gerencia'),	
	  ('Bryan Rogel','bryan.rogel','123','Admin'),				
	  ('Kenia Osorio','kenia.osorio','ke.os','Admin'),			
	  ('Luis Anaya','luis.anaya','lanaya12','Admin'),	
	  ('Antonio Hurtado','antonio.hurtado','hurtad01','Admin'),		-- ANTONIO HURTADO NIVEL ADMINISTRADOR		
	  ('Antonio Hurtado','ant.hurt','hurtad01','Gerencia'),			-- ANTONIO HURTADO NIVEL GERENCIA
	  ('Antonio Hurtado','ant.hurtado','hurtad01','Empleados'),		-- ANTONIO HURTADO NIVEL EMPLEADOS
	  ('Roberto Pineda','robert.pd','sasuk3','Gerencia'),         
      ('Diana Morales','diana.morales','fundre','Gerencia'),
	  ('Carlos Garcia','montes.carlos','Lanthea','Gerencia'),
	  ('Misael Alvarenga','misa1896','Radowco','Admin'),  
	  ('Sandra Avila','sandra.avila','sandrasplt','Empleados'),      
	  ('Gabriel Marquez','gaby9635','SisEour','Empleados'),                            
	  ('Rodrigo Hidalgo ','hidalgo20','OtSavel','Empleados'),
	  ('Roxana Pascual','rosadepas','Phrolov','Empleados'),
	  ('José Rubén','cheperrub3','SisEour','Empleados'),
	  ('Graciano Angelino','angelgra5','Linalot','Empleados'),
	  ('Mateo Obdulia','mattobdulia','Ornarch','Empleados'),
	  ('Virginia Imelda','virginia.jme','Inessit','Empleados'),
	  ('Renato Salvador','chamba896','RiatiEx','Gerencia'),
	  ('Amado Gerardo','amadm','TiressN','Gerencia'),
	  ('Claudia Nicodemo','nicod7563','Pocully','Gerencia'),
	  ('Augusto Joaquín','agosto.julio','Alliani','Gerencia'),
	  ('Nicolás Delia','delianico','Trovord','Empleados'),
	  ('Gabriel Dos Santos','gabby25','Athemac','Admin'),                          
	  ('Amalia Mariano','mariano563','DlUlamb','Admin'),
	  ('Africa Ciro','affciro2','WarEdro','Admin'),
	  ('Antonio Galo','antogg','LEctive','Admin'),             
	  ('Luciana Rolando','lucy865d','OaRyngA','Admin'),         
	  ('Esther Marisa','esmary398','AngilIc','Gerencia'),
	  ('Sosimo Evaristo','soseva764','Nitendy','Gerencia'),
	  ('Maricela Facundo','marff63','OidaiSe','Gerencia'),
	  ('Marcia Estela','estemarc563','PulleRa','Gerencia'),
	  ('Placido Raquel','plashrql113','HyRiach','Gerencia'),
	  ('Patricio Hilario','patrickH4','LiiGger','Empleados'),
	  ('Ana Alejo','alejon025','StanIri','Empleados'),
	  ('Ulises Martina','ulises.martina','EnwieIt','Empleados'),                            
	  ('Dario Leonor','darkleo8','OadicZe','Empleados'),
	  ('Teresa Martinez','latere76','Oncemon','Empleados'),
	  ('Albino Emiliano','albbml9','Nidacto','Empleados'),
	  ('Samuel Martirio','sammywq','EssEron','Empleados'),
	  ('Rosenda Valero','rosvl.ero','Shecumb','Empleados'),
	  ('Eva Salome','sal2eva','Masocke','Empleados'),
	  ('Tamara Nataniel','tama.nataniel','Helpati','Admin'),      
	  ('Leandro Ascension','leandro.asc','CDisele','Empleados'),
	  ('Ignacio Estrella','estrelladm','Ushlasi','Empleados'),
	  ('Casandra Salvador','cassandraff','Ctangua','Gerencia'),
	  ('Renato Dimas','dimas.renato','Lolcasm','Admin'),         
	  ('Natalia Alba','alba.pnt','Quirise','Empleados'),                                   
	  ('Felipe Celestina','filepinocl','VeTeral','Admin'),        
	  ('Isaias Casimiro','isa.casim','CentTio','Empleados');


-- -> REGISTROS PARA TABLA EMPLEADOS
GO
insert into Empleados
values('JCDLSPT01','Julio Cesar','Dominguez Lopez','m','jcdl12@speedlimit.com','10-05-2010','055321-5','0614-221067-111-1',650.22),
	  ('JCDLSPT02','Rajah Lionel','Mueller Langley','m','rajahlio653@speedlimit.com','12-09-2010','093114-9','0614-231097-111-2',700.22),
	  ('JCDLSPT03','Wyatt Cruz','Mays Good','m','wyattcrz352@speedlimit.com','22-02-1999','026321-4','0614-294267-111-8',563.12),
	  ('JCDLSPT04','Peter Ali','Perez Wyatt','m','kaknovurdu@speedlimit.com','12-08-2009','051231-5','0614-228747-111-4',680.27),
	  ('JCDLSPT05','Kevin Dorian','Powell Strickland','m','yjakysowe8877@speedlimit.com','08-04-2018','093321-3','0614-121527-111-2',703.22),
	  ('JCDLSPT06','Dennis Thane','Soto Keith','m','yjakysowe@speedlimit.com','10-09-2010','055321-5','0614-296267-111-2',650.22),
	  ('JCDLSPT07','Eagan Garth','Collier Blackburn','m','offusurra-3290@speedlimit.com','15-10-2003','098321-5','0614-221210-111-3',602.22),
	  ('JCDLSPT08','John Mason','Lawrence Riddle','m','yhaniqeba89@speedlimit.com','20-12-2011','055853-5','0614-286367-111-4',668.22),
	  ('JCDLSPT09','Alan Bradley','Fields Gutierrez','m','azopanoq6077@speedlimit.com','17-11-1999','055125-7','0614-221856-111-8',670.22),
	  ('JCDLSPT10','Quamar Benjamin','Solis Landry','m','ecyvirad-7789@speedlimit.com','19-03-2015','086321-5','0614-890746-111-3',650.22),
	  ('JCDLSPT11','Knox Beck','Hanson Eaton','f','ecyvir7763@speedlimit.com','25-01-2007','052036-8','0614-285934-111-2',650.10),
	  ('JCDLSPT12','Warren Brendan','Carrillo Avila','f','ecyvirad863@speedlimit.com','05-06-2014','069617-3','0614-447842-111-1',650.22),
	  ('JCDLSPT13','Chancellor Tanner','Maxwell Callahan','m','etammucadi2872@speedlimit.com','16-09-2001','058951-5','0614-395347-111-1',650.22),
	  ('JCDLSPT14','Ezra Rashad','Hendricks Morales','f','udykemoq1055@speedlimit.com','20-09-2008','089531-5','0614-059120-111-1',650.22),
	  ('JCDLSPT15','Tanner Blake','Blake Mcdowell','m','udykem105@speedlimit.com','02-03-2015','057536-5','0614-500309-111-1',650.22),
	  ('JCDLSPT16','Honorato Caldwell','Alston Ferrell','m','ajesinneffy748@speedlimit.com','16-09-2018','050421-5','0614-628926-111-1',650.22),
	  ('JCDLSPT17','Armando Cade','Joyner Cobb','m','kesunnille085@speedlimit.com','21-05-2000','050421-5','0614-633014-111-1',650.22),
	  ('JCDLSPT18','Reuben Conan','Fitzgerald Guerra','m','sidiwo9345@speedlimit.com','08-11-2009','004321-5','0614-330667-111-1',650.22),
	  ('JCDLSPT19','Stewart Nicholas','Melton Lynn','m','mp41510@speedlimit.com','26-06-2010','050441-5','0614-231690-111-1',650.22),
	  ('JCDLSPT20','Grady Galvin','Harper Reyes','m','bukceline@speedlimit.com','06-07-2013','004521-5','0614-388792-111-1',650.22),
	  ('JCDLSPT21','Myles Ignatius','Ballard Pennington','m','zarlengod@speedlimit.com','20-09-1999','004621-5','0614-776579-111-1',650.22),
	  ('JCDLSPT22','Hector Harlan','Brady Bowen','m','sandeep.slg678@speedlimit.com','13-12-2005','050471-5','0614-375290-111-1',650.22),
	  ('JCDLSPT23','Boris Chancellor','Velazquez Jimenez','m','noramfuller@speedlimit.com','05-11-2008','050481-5','0614-314713-111-1',650.22),
	  ('JCDLSPT24','Tarik Slade','English Moses','m','bangtoyib2@speedlimit.com','19-09-2007','050491-5','0614-685734-111-1','650.22'),
	  ('JCDLSPT25','Rooney Carter','Huff Valentine','m','andersonasantos@speedlimit.com','14-02-2010','050501-5','0614-937863-111-1',650.22),
	  ('JCDLSPT26','Ronan Nash','Pope Vargas','m','aubreybyfield@speedlimit.com','20-11-2003','050511-5','0614-993520-111-1',650.22),
	  ('JCDLSPT27','Mia Thomas','Estes Griffith','f','alesganz@speedlimit.com','18-04-2007','050521-5','0614-256211-111-1',650.22),
	  ('JCDLSPT28','Salvador Wyatt','Mccormick Golden','m','ian.mcgrenaghan@speedlimit.com','19-03-2005','050531-5','0614-766596-111-1',650.22),
	  ('JCDLSPT29','Chancellor Hayden','Cervantes Kent','m','sanquel21@speedlimit.com','23-08-1998','050541-5','0614-329878-111-1',650.22),
	  ('JCDLSPT30','Peter Gareth','Aguirre Espinoza','m','atkins618@speedlimit.com','20-07-2009','050551-5','0614-364068-111-1',650.22),
	  ('JCDLSPT31','Baxter Wing','Jimenez Reilly','m','28youngpills@speedlimit.com','08-10-2015','050561-5','0614-805321-111-1',650.22),
	  ('JCDLSPT32','Jerry Elton','Robles Justice','m','armanddarkmoon08@speedlimit.com','20-09-2018','050571-5','0614-878200-111-1',650.22),
	  ('JCDLSPT33','George Jack','Chaney Hicks','m','wood0987@speedlimit.com','08-07-2014','050581-5','0614-519992-111-1',650.22),
	  ('JCDLSPT34','Salvador Odysseus','Washington Benjamin','m','bloodsik@speedlimit.com','19-01-2017','050591-5','0614-749812-111-1',700.22),
	  ('JCDLSPT35','Thomas Grant','Farley Fletcher','m','naidu.sainath@speedlimit.com','29-11-2011','050601-5','0614-550000-111-1',650.22),
	  ('JCDLSPT36','Otto Reece','Decker Bright','m','zara.athrun@speedlimit.com','13-06-2006','050621-5','0614-264110-111-1',650.22),
	  ('JCDLSPT37','Jarrod Amal','Stone Mills','m','got2scrap@speedlimit.com','02-12-2003','050631-5','0614-158189-111-1',650.22),
	  ('JCDLSPT38','Gabriel Nero','Rivera Frank','m','tigervod@speedlimit.com','10-01-2018','050641-5','0614-169267-111-1',650.22),
	  ('JCDLSPT39','Lena Rahim','Paul Strickland','f','christinatan03@speedlimit.com','20-10-1996','050651-5','0614-323340-111-1',550.22),
	  ('JCDLSPT40','Honorato Logan','Clarke Kemp','m','sp031d2330@speedlimit.com','06-08-1998','050661-5','0614-772712-111-1',650.22),
	  ('JCDLSPT41','Scott Camden','Valenzuela Mueller','m','meetakhi@speedlimit.com','20-05-1999','050671-5','0614-565634-111-1',650.22),
	  ('JCDLSPT42','Felix Noble','Wolf Walsh','m','lundpb@speedlimit.com','04-02-2006','050681-5','0614-758752-111-1',650.22),
	  ('JCDLSPT43','Lewis Lamar','Reilly Long','m','lukegrable@speedlimit.com','16-12-2010','050691-5','0614-302447-111-1',650.22),
	  ('JCDLSPT44','Chester Chaney','Bond Carlson','f','powets8632@speedlimit.com','21-09-2007','050701-5','0614-605769-111-1',620.22),
	  ('JCDLSPT45','Acton Guy','Faulkner Mcneil','m','ali.sahabb.saina@speedlimit.com','14-03-2011','050711-5','0614-829004-111-1',650.22),
	  ('JCDLSPT46','Palmer Tobias','Horton Lopez','m','ichsan.maulana1975@speedlimit.com','26-08-2015','050721-5','0614-424366-111-1',650.22),
	  ('JCDLSPT47','Cain Tyler','Lamb Gutierrez','m','timeka80@speedlimit.com','21-02-2008','050731-5','0614-548772-111-1',650.22),
	  ('JCDLSPT48','Macaulay Jelani','Davidson Pittman','f','joxielpiss@speedlimit.com','08-07-2012','050741-5','0614-548886-111-1',650.22),
	  ('JCDLSPT49','Seth Griffin','Lang Mullins','m','estaberry@speedlimit.com','04-03-2013','050751-5','0614-359499-111-1',650.22),
	  ('JCDLSPT50','Hayes Rahim','Oneill Strickland','m','ashish1183@speedlimit.com','18-08-2001','050761-5','0614-475260-111-1',690.22);


-- -> REGISTROS PARA TABLA PROVEEDORES
GO
insert into ProveedoresProdutos
values('PR-ZMVYCZ','SUPER REPUESTOS S.A DE C.V','Repuestos Automotrices','Colonia Escalon. Av. Revolucion No. 666','2556-1357','superrepuestoscentroservicio@contacto.com'),
	  ('PR-WVNFXW','CORPORACION AUTOMOTRIZ S.A DE C.V','Repuestos Automotrices','Colonia Buenos Aires 3, Diagonal Centroamérica, Avenida Alvarado, contiguo al Ministerio de Hacienda','2250-0007','corporacionautomotrizcentroservicio@contacto.com'),
	  ('PR-BTWRHE','AUTO REPUESTOS RAMOS S.A DE C.V','Repuestos Automotrices','1ª Calle Poniente entre 60ª Avenida Norte y Boulevard Constitución No. 3549','2201-8343','autorepuestosramoscentroservicio@contacto.com'),
	  ('PR-DUKWES','INTERNACIONAL DE REPUESTOS S.A DE C.V','Repuestos Automotrices','Colonia San Francisco, Avenida Las Camelias y Calle Los Abetos No. 21','2531-1337','internacionalderepuestoscentroservicio@contacto.com'),
	  ('PR-TPGXKY','DACOMSA S.A DE C.V','Taller Automotriz','10ª Avenida Sur y Calle Lara No. 934, Barrio San Jacinto','2290-8455','dacomsacentroservicio@contacto.com'),
	  ('PR-QVMLDH','INDUSTRIAS KIRKWOOD S.A DE C.V','Repuestos Automotrices','Avenida Independencia y Alameda Juan Pablo II, No. 437','2524-5406','industriaskirkwoodcentroservicio@contacto.com'),
	  ('PR-MAAMVE','T&E INTERNATIONAL TRADERS CO S.A DE C.V','Repuestos Automotrices','Alameda Juan Pablo II y Avenida Cuscatancingo No 320 San Salvador, El Salvador','2225-6088','t&einternationaltraderscocentroservicio@contacto.comcentroservicio@contacto.com'),
	  ('PR-VSLJCZ','LIAONING AUTOMOTIVE ZONE MFG S.A DE C.V','Repuestos Automotrices','Boulevard República Federal de Alemania, # 122','2193-3434','liaoningautomotivezonemfgcentroservicio@contacto.com'),
	  ('PR-DXWZAN','REPRESENTACIONES R&G S.A DE C.V','Repuestos Automotrices','25 AV. Sur y Calle Gerardo Barrios #640','2592-9053','representacionesr&gcentroservicio@contacto.com'),
	  ('PR-XVGBWU','INCAPROSA S.A DE C.V','Repuestos Automotrices','Avenida Bernal, Block B, Casa #15, Urbanización Yumury','2525-3989','incaprosacentroservicio@contacto.com'),
	  ('PR-EUPLHA','JDC INTERNATIONAL GROUP S.A DE C.V','Taller Automotriz','Col Layco 29 Cl Pte y 21 Av Norte 1204','2530-0594','jdcinternationalgroupcentroservicio@contacto.com'),
	  ('PR-MRVRFX','ABRACINTAS Y ADHESIVOS S.A DE C.V','Repuestos Automotrices','2da Ave Nte #1627 Contiguo A Col Rabida, San Salvador','2238-5388','abracintasadhesivoscentroservicio@contacto.com'),
	  ('PR-NVWLPT','PROTECNICA S.A DE C.V','Repuestos Automotrices','Km 73 1/2 Cantón Upatoro, Caserio las Mesas, Chalatenango','2564-3978','protecnicacentroservicio@contacto.com'),
	  ('PR-KETBXD','PROMIXAR S.A DE C.V','Repuestos Automotrices','Alameda Juan Pablo II 7 Av Sur Edificio Gerardo Barrios L-10','2522-5066','promixarcentroservicio@contacto.com'),
	  ('PR-TEBABM','DICONS S.A DE C.V','Repuestos Automotrices','Col Lorena Cl Vista Hermosa No 6 Mejicanos','2261-0449','diconscentroservicio@contacto.com'),
	  ('PR-HBFWCN','SHENZHEN AIDELY ELECTROMECHANICAL S.A DE C.V','Repuestos Automotrices','Residencia Los Próceres Autopista Sur No 16','2270-5160','shenzhenaidelyelectromechanicalcentroservicio@contacto.com'),
	  ('PR-LQJUQS','NINGBO GIAN CHIYANG TECH S.A DE C.V','Taller Automotriz','Col Mónico Av 14 De Julio No 37 Mejicanos','2530-4126','ningbotechcentroservicio@contacto.com'),
	  ('PR-PZLHDC','MILESUN RUBBER & PLASTIC TECHNOLOGY S.A DE C.V','Repuestos Automotrices','91 Avenida N. 515 Edificio Abacus Colonia Escalón','2225-4815','milesuntechnologycentroservicio@contacto.com'),
	  ('PR-JSZSQC','VOLCORM S.A DE C.V','Repuestos Automotrices','Col Antekirta Km 4 1/2 Blvd del Ejército','2529-2032','volcormcentroservicio@contacto.com'),
	  ('PR-NVYYJU','ABY C S.A DE C.V','Repuestos Automotrices','Calle Antigua a Tonacatepeque No 122','2585-8538','abbycentroservicio@contacto.com');

-- -> REGISTROS PARA TABLA CATEGORIA DE PRODUCTOS
GO
insert into CategoriaProductos
values ('Frenos y Discos','Todo el sistema de frenos automotriz liviano y pesados'),
	   ('A/C y Ventilacion','Todo el sistema de aire acondicionado automotriz'),
	   ('Radiadores','Todo el sistema de refrigeracion automotriz'),
		('Sistemas de parachoques','En las últimas décadas los parachoques, 
		que antes eran simples componentes de protección contra el impacto, 
		han evolucionado hasta convertirse en significativos elementos de diseño 
		y soluciones de seguridad de alta tecnología para los automóviles.'),
	   ('Sistemas para exteriores','Nuestro espectro de productos del área Exterior Systems abarca desde los alerones traseros, 
		las taloneras o los guardabarros hasta los faldones y las cubiertas.'),
	   ('Sistemas de sellado','Nuestras soluciones aportan un confort esencial para la conducción. En calidad de elementos funcionales, 
		nuestras juntas no tan sólo impiden que el polvo, la suciedad y el agua penetren en el techo, las ventanas o los parabrisas delantero 
		y trasero sino que además seducen por su diseño.'),
		('Sistemas de conducción de aire','Nuestros componentes contribuyen a aumentar el confort en el interior de los vehículos. 
		Gracias al empleo de modernas herramientas de simulación y desarrollo garantizamos un concepto eficiente de la conducción del aire y 
		lo implementamos de modo óptimo en nuestros componentes.'),
		('Sistemas de conducción de agua','Nuestros productos facilitan el óptimo transporte del agua para la limpieza de la luneta delantera y trasera. 
		Las soluciones de productos calefactados garantizan la funcionalidad del transporte del agua incluso en el caso de heladas.');

-- -> REGISTROS PARA TABLA PRODUCTOS SPEEDLIMIT TALLER S.A DE C.V
GO
insert into Productos
values('7-411000120700','Pastillas de Freno Ceramica','BOSH','CERAMIC BOSH PREMIUM',99.95,'ABYC S.A DE C.V','Frenos y Discos'),
	  ('7-411000120701','Largueros guia en acero inoxidable','AIRTEX','SP-011',20.00,'SUPER REPUESTOS S.A DE C.V','Sistemas de parachoques'),    
	  ('7-411000120702','Proteccion antichoque para congeladores','BANDO','SP-012',70.00,'CORPORACION AUTOMOTRIZ S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120703','Parachoques summit','BANDO','SP-013',80.00,'AUTO REPUESTOS RAMOS S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120704','Parachoques summit sahara','AIRTEX','SP-014',64.00,'SUPER REPUESTOS S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120705','Parachoques deluxe','BANDO','SP-015',69.00,'AUTO REPUESTOS RAMOS S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120706','Parachoques deluxe sahara','DAYCO','SP-016',90.00,'CORPORACION AUTOMOTRIZ S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120707','Parachoques stubby','BANDO','SP-017',92.00,'VOLCORM S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120708','Protecciones laterales','AIRTEX','SP-018',95.00,'NINGBO GIAN CHIYANG TECH S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120709','Protector de bajos','DAYCO','SP-019',64.00,'MILESUN RUBBER & PLASTIC TECHNOLOGY S.A DE C.V','Sistemas de parachoques'),
	  ('7-411000120710','Parachoque trasero & portaruedas','BANDO','SP-020',67.00,'SUPER REPUESTOS S.A DE C.V','Sistemas de parachoques'),
	  ('7-412000244801','Alerones traseros','DAYCO','SE-011',70.00,'INTERNACIONAL DE REPUESTOS S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244802','Facias','BANDO','SE-012',45.00,'DACOMSA S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244803','Moldura de cajuela','DAYCO','SE-013',85.00,'ABY C S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244804','Guardafangos de plástico','AIRTEX','SE-014',47.50,'ABY C S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244805','Cubiertas protectoras','DAYCO','SE-015',69.95,'INTERNACIONAL DE REPUESTOS S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244806','Perfiles protectores laterales','UNION JAPAN','SE-016',43.85,'INDUSTRIAS KIRKWOOD S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244807','Pasaruedas','US MOTOR WORKS','SE-017',73.50,'MILESUN RUBBER & PLASTIC TECHNOLOGY S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244808','Perfil para panel de puerta','NPW','SE-018',68.50,'CORPORACION AUTOMOTRIZ S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244809','Kit de Cepillos de Detallado','US MOTOR WORKS','SE-019',61.50,'VOLCORM S.A DE C.V','Sistemas para exteriores'),
	  ('7-412000244810','Limpiador de partes plasticas','NPW','SE-020',78.50,'INDUSTRIAS KIRKWOOD S.A DE C.V','Sistemas para exteriores'),
      ('7-413000344801','Cañuelas','NPW','SS-011',56.90,'CORPORACION AUTOMOTRIZ S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344802','Perfiles para techo','TYC','SS-012',73.45,'INTERNACIONAL DE REPUESTOS S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344803','Marcos de parabrisas','UNION JAPAN','SS-013',30.00,'SUPER REPUESTOS S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344804','Perfil inferior de parabrisas','NPW','SS-014',35.10,'T&E INTERNATIONAL TRADERS CO S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344805','Perfiles vierteaguas o perfiles laterales','TYC','SS-015',60.50,'LIAONING AUTOMOTIVE ZONE MFG S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344806','Selladores de roscas','US MOTOR WORKS','SS-016',50.50,'REPRESENTACIONES R&G S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344807','Selladores de uso general','NPW','SS-017',62.50,'T&E INTERNATIONAL TRADERS CO S.A DE C.V','Sistemas de sellado'),
      ('7-413000344808','Perfiles para techo ufgh','TYC','SS-018',69.00,'REPRESENTACIONES R&G S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344809','Perfil inferior de parabrisas terson','NPW','SS-019',72.50,'LIAONING AUTOMOTIVE ZONE MFG S.A DE C.V','Sistemas de sellado'),
	  ('7-413000344810','Selladores de roscas urbel','US MOTOR WORKS','SS-020',68.50,'T&E INTERNATIONAL TRADERS CO S.A DE C.V','Sistemas de sellado'),
	  ('7-413000444801','Ductos de aire para el interior de los vehículos','STANDARD','SCA-011',89.95,'LIAONING AUTOMOTIVE ZONE MFG S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444802','Condensadores ultra','STANDARD','SCA-012',80.15,'LIAONING AUTOMOTIVE ZONE MFG S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444803','Compresoras rolart','WALKER','SCA-013',82.25,'REPRESENTACIONES R&G S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444804','Filtros deshidratantes','KYOSAN','SCA-014',70.95,'INCAPROSA S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444805','Valvula de expansion','STANDARD','SCA-015',65.40,'JDC INTERNATIONAL GROUP S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444806','Evaporador','WALKER','SCA-016',70.45,'INCAPROSA S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444807','Presostatos e interruptores','BERAL','SCA-017',64.45,'JDC INTERNATIONAL GROUP S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444808','Ventiladores','WALKER','SCA-018',81.95,'ABRACINTAS Y ADHESIVOS S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444809','Empalmes y manguitos','STANDARD','SCA-019',73.75,'PROTECNICA S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000444810','Ventilador del condensador','KYOSAN','SCA-020',82.25,'INCAPROSA S.A DE C.V','Sistemas de conducción de aire'),
	  ('7-413000544801','Sistema limpiaparabrisas','STANDARD','SCAG-011',60.50,'ABRACINTAS Y ADHESIVOS S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544802','Sistema modular de retención pluvial','WALKER','SCAG-012',68.50,'PROTECNICA S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544803','Drenaje para grandes áreas de techo','TOT PISTONS','SCAG-013',55.99,'PROMIXAR S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544804','Drenaje de ventilación activa','NDC','SCAG-014',68.50,'DICONS S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544805','Carcasa del ventilador','NACHI','SCAG-015',59.50,'PROMIXAR S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544806','Sistema de refrigeración de motor','TENACITY','SCAG-016',64.50,'DICONS S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544807','Junta de la tuberia del refrigerante','KYB','SCAG-017',68.60,'SHENZHEN AIDELY ELECTROMECHANICAL S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544808','Sistema de tipo cerrado (de líquido)','MINTEX','SCAG-018',69.80,'NINGBO GIAN CHIYANG TECH S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544809','Sistema de tipo abierto (de aire)','TRATAUTO','SCAG-019',70.20,'SHENZHEN AIDELY ELECTROMECHANICAL S.A DE C.V','Sistemas de conducción de agua'),
	  ('7-413000544810','Sistema de tipo combinado','MANDO','SCAG-020',73.90,'NINGBO GIAN CHIYANG TECH S.A DE C.V','Sistemas de conducción de agua');

-- -> REGISTROS PARA TABLA TIENDAS SPEEDLIMIT TALLER S.A D C.V
GO
insert into TiendasSpeedLimit
values('SPT001A','Urb. Madreselva Calle El Espino, No 118 Antiguo Cuscatlan','2551-5357'),
	  ('SPT002B','25 Av. Sur y calle Gerardo Barrios No.640. San Salvador','2252-5207'),
	  ('SPT003C','Kilómetro 12 ½ Carretera Troncal del Norte. Apopa','2203-5143'),
	  ('SPT004D','4ª Av. Norte y 5ª Calle Ote. Cojutepeque','2534-6337'),
	  ('SPT005E','Blvd. Constitución, 50 mts. antes de gasolinera Puma Juan Pablo II, San Antonio Abad, SS','2295-6255'),
	  ('SPT006F','Cantón Aldeita, Caserío Amayo, Jurisdicción de Tejutla, Chalatenango','2526-6106'),
	  ('SPT007G','25 Av. Sur y calle Gerardo Barrios No.640. San Salvador','2227-7388'),
	  ('SPT008H','8ª Calle Poniente y Avenida Carlos Bonilla Sur, Barrio El Calvario, Ilobasco','2198-7234'),
	  ('SPT009I','Calle Concepción # 1113 Barrio Cisneros. San Salvador','2599-7153'),
	  ('SPT010J','Km 99 1/2, Carretera Panamericana, Colonia Santa Maria Ahuachapán','2510-4389'),
	  ('SPT011K','Bo. Las Animas, 7a. Av. Norte, Calle a El Trapiche #37, Chalchuapa','2511-4294'),
	  ('SPT012L','5ª Calle Oriente y Calle Anguiatú, Metapán, Santa Ana','2212-4188'),
	  ('SPT013M','Final 25av. Sur y antigua calle a Santa Ana','2513-3378'),
	  ('SPT014N','Suburbios del Barrio Honduras, 3ra Calle Poniente, Contiguo a Escuela República de Honduras, La Unión','2514-3266'),
	  ('SPT015P','Ruta Militar, Final 7ª Avenida Norte, San Miguel.','2215-3149');

-- -> REGISTROS PARA TABLA ENCARGADOS DE TIENDAS SPEEDLIMIT TALLER S.A D C.V
GO
insert into EncargadosTiendas
values('1','Julio Cesar','Dominguez Lopez','1','SPT001A','Urb. Madreselva Calle El Espino, No 118 Antiguo Cuscatlan','2295-1543'),
	  ('2','Rajah Lionel','Mueller Langley','2','SPT002B','25 Av. Sur y calle Gerardo Barrios No.640. San Salvador','2252-5207'),
	  ('3','Wyatt Cruz','Mays Good','3','SPT003C','Kilómetro 12 ½ Carretera Troncal del Norte. Apopa','2203-5143'),
		('4','Peter Ali','Perez Wyatt','4','SPT004D','4ª Av. Norte y 5ª Calle Ote. Cojutepeque','2534-6337'),
		('5','Kevin Dorian','Powell Strickland','5','SPT005E','Blvd. Constitución, 50 mts. antes de gasolinera Puma Juan Pablo II, San Antonio Abad, SS','2295-6255'),
		('6','Dennis Thane','Soto Keith','6','SPT006F','Cantón Aldeita, Caserío Amayo, Jurisdicción de Tejutla, Chalatenango','2526-6106'),
		('7','Eagan Garth','Collier Blackburn','7','SPT007G','25 Av. Sur y calle Gerardo Barrios No.640. San Salvador','2227-7388'),
		('8','John Mason','Lawrence Riddle','8','SPT008H','8ª Calle Poniente y Avenida Carlos Bonilla Sur, Barrio El Calvario, Ilobasco','2198-7234'),
		('9','Alan Bradley','Fields Gutierrez','9','SPT009I','Calle Concepción # 1113 Barrio Cisneros. San Salvador','2599-7153'),
		('10','Quamar Benjamin','Solis Landry','10','SPT010J','Km 99 1/2, Carretera Panamericana, Colonia Santa Maria Ahuachapán','2510-4389'),
		('11','Knox Beck','Hanson Eaton','11','SPT011K','Bo. Las Animas, 7a. Av. Norte, Calle a El Trapiche #37, Chalchuapa','2511-4294'),
		('12','Warren Brendan','Carrillo Avila','12','SPT012L','5ª Calle Oriente y Calle Anguiatú, Metapán, Santa Ana','2212-4188'),
		('13','Chancellor Tanner','Maxwell Callahan','13','SPT013M','Final 25av. Sur y antigua calle a Santa Ana','2513-3378'),
		('14','Ezra Rashad','Hendricks Morales','14','SPT014N','Suburbios del Barrio Honduras, 3ra Calle Poniente, Contiguo a Escuela República de Honduras, La Unión','2514-3266'),
		('15','Tanner Blake','Blake Mcdowell','15','SPT015P','Ruta Militar, Final 7ª Avenida Norte, San Miguel.','2215-3149');


/*********************************************************************************************************************************************
											FIN INSERTANDO REGISTROS A LAS TABLAS DEL SISTEMA
*********************************************************************************************************************************************/


/*******************************************************************************************************************************************
											CONSULTAS GENERALES DE TABLAS DEL SISTEMA
********************************************************************************************************************************************/

-- -> PROCEDIMIENTOS EXTRAS
select *from usuario				-- USUARIOS {LOGIN DEL SISTEMA}
select *from Empleados				-- EMPLEADOS
select *from Productos				-- PRODUCTOS SPEEDLIMIT TALLER S.A DE C.V
select *from ProveedoresProdutos	-- PROVEEDORES DE PRODUCTOS
select *from CategoriaProductos		-- CATEGORIA DE PRODUCTOS
select *from ControlInventarios		-- CONTROL DE INVENTARIOS
select *from TiendasSpeedLimit		-- TIENDAS SPEEDLIMIT TALLER S.A DE C.V
select *from EncargadosTiendas		-- ENCARGADOS TIENDAS SPEEDLIMIT TALLER S.A DE C.V


/*******************************************************************************************************************************************
											FIN CONSULTAS GENERALES DE TABLAS DEL SISTEMA
********************************************************************************************************************************************/



/*******************************************************************************************************************************************
													DEPURACION DE TABLAS DEL SISTEMA
********************************************************************************************************************************************/

-- -> PROCEDIMIENTOS EXTRAS
drop table EncargadosTiendas					-- ENCARGADOS TIENDAS SPEEDLIMIT TALLER S.A DE C.V
drop table TiendasSpeedLimit					-- TIENDAS SPEEDLIMIT TALLER S.A DE C.V
drop table ControlInventarios					-- CONTROL DE INVENTARIOS
drop table CategoriaProductos					-- CATEGORIAS PRODUCTOS
--drop table ProveedoresProductos				-- PROVEEDORES PRODUCTOS -> IMPORTANTE: ESTA DEBE SER BORRADA DIRECTAMENTE DE LAS TABLAS DEL SGBD SQL
drop table Productos							-- PRODUCTOS SPEEDLIMIT TALLER S.A DE C.V
drop table Empleados							-- EMPLEADOS
drop table usuario								-- USUARIOS

/*******************************************************************************************************************************************
												     FIN DEPURACION DE TABLAS DEL SISTEMA
********************************************************************************************************************************************/


/**
    @@@@  @@@@  @@@@  @@@@  @@@@  @     @@@@  @@    @@  @@@@  @@@@@@    @@@@  @@@@@  @     @     @@@@  @@@@@@
    @@    @  @  @     @     @  @  @      @@   @@ @@ @@   @@    @@        @@   @@ @@  @     @     @     @@ @@@
    @@@@  @@@@  @@@@  @@@@  @  @  @      @@   @@    @@   @@    @@        @@   @@@@@  @     @     @@@@  @@@@@@
      @@  @@    @     @     @  @  @      @@   @@    @@   @@    @@        @@   @@ @@  @     @     @     @@  @@
    @@@@  @@    @@@@  @@@@  @@@@  @@@@  @@@@  @@    @@  @@@@   @@        @@   @@ @@  @@@@  @@@@  @@@@  @@   @@
**/