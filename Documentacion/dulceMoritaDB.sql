create database dulce_morita;
--drop database dulce_morita;
use dulce_morita;
create table producto(
	id_producto int NOT NULL identity(1,1),
	nombre varchar(100),
	constraint pk_persona primary key (id_producto)
);
create table orden_produccion(
	id_orden int identity(1,1) not null primary key,
	produccion_total int not null,
	fecha_creacion varchar(100) not null,
	fk_producto int foreign key references producto(id_producto)
);
create table operario(
	id_operario int identity(1,1) primary key not null,
	nombre_completo varchar(100) not null
);
create table lote_produccion(
	id_lote int identity(1,1) primary key not null,
	fk_orden int foreign key references orden_produccion(id_orden),
	cantidad_produccion int not null,
	fecha_registro varchar(100) not null
);
create table notificacion(
	id_notificacion int identity(1,1) primary key not null,
	fk_lote int foreign key references lote_produccion(id_lote),
	fk_ope int foreign key references operario(id_operario),
	buenas int not null,
	malas int not null,
	f_inicio varchar(100) not null,
	f_fin varchar(100) not null,
	gastos_adicionales int not null,
	obseraciones varchar(200)
);
insert into dbo.operario(nombre_completo) values ('Carlos'),('Rafael'),('Viviana');
insert into dbo.producto(nombre) values ('Gomitas'),('Frunas'),('Galletas'),('Sparkies'),('BomBom-Bum');