CREATE DATABASE autocinema;
GO
USE autocinema;
GO
CREATE TABLE peliculas(
id int not null IDENTITY(1,1),
nombre varchar(45) not null,
duracion int not null,
clasificacion char(5) not null
CONSTRAINT PK_peliculas PRIMARY KEY(id)
);

GO
CREATE TABLE clientes(
id int not null IDENTITY(1,1),
matriculaAuto varchar(15),
marcaAuto varchar(45)
CONSTRAINT PK_clientes PRIMARY KEY(id)
);drop table clientes;
GO
CREATE TABLE productos_comestibles(
id int not null IDENTITY(1,1),
nombre varchar(45) not null,
precio float
CONSTRAINT PK_productos_comestibles PRIMARY KEY(id)
);
GO
CREATE TABLE horarios(
id int not null IDENTITY(1,1),
idPelicula int,
fecha date not null,
hora time not null
CONSTRAINT PK_horarios PRIMARY KEY(id)
CONSTRAINT FK_horarios_peliculas FOREIGN KEY(idPelicula) REFERENCES peliculas(id) 
);
GO
CREATE TABLE boletos(
folio int not null,
idCliente int ,
idHorario int ,
CONSTRAINT PK_boletos PRIMARY KEY(folio),
CONSTRAINT FK_boletos_clientes FOREIGN KEY(idCliente) REFERENCES clientes(id),
CONSTRAINT FK_boletos_horarios FOREIGN KEY(idHorario) REFERENCES horarios(id)

);
GO
CREATE TABLE ventas_comestibles(
folioVenta int not null,
idCliente int,
total float,
CONSTRAINT PK_ventas_comestibles PRIMARY KEY(folioVenta),
CONSTRAINT FK_ventas_comestibles_clientes FOREIGN KEY(idCliente) REFERENCES clientes(id)
);
GO

CREATE TABLE detalles_producto(
idProducto int not null,
folioVenta int not null,
cantidad int,
precio float,
CONSTRAINT PK_detalles_producto PRIMARY KEY(idProducto,folioVenta),
CONSTRAINT FK_detalles_producto_productos FOREIGN KEY(idProducto) REFERENCES productos_comestibles(id),
CONSTRAINT FK_detalles_producto_ventas FOREIGN KEY(folioVenta) REFERENCES ventas_comestibles(folioVenta)
);
GO

--------------------------------------------------------------------Procs

-------peliculas-----------
CREATE PROC InsertPelicula
@nombre varchar(45),
@duracion int,
@clasificacion char(5) 
AS
INSERT INTO peliculas values(@nombre,@duracion,@clasificacion);
GO

CREATE PROC UpdatePelicula
@id int,
@nombre varchar(45),
@duracion int,
@clasificacion char(5)
AS 
UPDATE peliculas SET nombre=@nombre,duracion=@duracion,clasificacion=@clasificacion WHERE id=@id;
GO

CREATE PROC ConsultPelicula
@id int
AS 
SELECT * FROM peliculas WHERE id=@id;
GO

CREATE PROC ConsultPeliculas
AS
SELECT * FROM peliculas;
GO

CREATE PROC DeletePelicula
@id int
AS
DELETE FROM peliculas WHERE id=@id;
GO

-----------clientes---------------
CREATE PROC InsertCliente
@matriculaAuto varchar(15),
@marcaAuto varchar(45)
AS
INSERT INTO clientes values(@matriculaAuto,@marcaAuto);
GO

CREATE PROC UpdateCliente
@id int,
@matriculaAuto varchar(15),
@marcaAuto varchar(45)
AS
UPDATE clientes SET matriculaAuto=@matriculaAuto, marcaAuto=@marcaAuto WHERE id=@id;
GO

CREATE PROC ConsultCliente
@id int
AS
SELECT * FROM clientes WHERE id=@id;
GO

CREATE PROC ConsultClientes
AS
SELECT * FROM clientes;
GO

CREATE PROC DeleteCliente
@id int 
AS
DELETE FROM clientes WHERE id=@id;
GO

-------productos_comestibles--------
CREATE PROC InsertProductoComestible
@nombre varchar(45),
@precio float
AS
INSERT INTO productos_comestibles VALUES(@nombre,@precio);
GO

CREATE PROC UpdateProductoComestible
@id int,
@nombre varchar(45),
@precio float
AS
UPDATE productos_comestibles SET nombre=@nombre,precio=@precio WHERE id=@id;
GO

CREATE PROC ConsultProductoComestible 
@id int
AS
SELECT * FROM productos_comestibles WHERE id=@id;
GO

CREATE PROC ConsultProductosComestibles
AS
SELECT * FROM productos_comestibles;
GO

CREATE PROC DeleteProductoComestible
@id int
AS
DELETE FROM productos_comestibles WHERE id=@id;
GO


------------horarios----------------------------
CREATE PROC InsertHorario
@idPelicula int,
@fecha date,
@hora time 
AS
INSERT INTO horarios VALUES(@idPelicula,@fecha,@hora);
GO

CREATE PROC UpdateHorario
@id int,
@idPelicula int,
@fecha date,
@hora time
AS
UPDATE horarios SET idPelicula=@idPelicula,fecha=@fecha,hora=@hora WHERE id=@id;
GO

CREATE PROC ConsultHorario
@id int
AS
SELECT * FROM horarios WHERE id=@id;
GO

CREATE PROC ConsultHorarios
AS
SELECT * FROM horarios;
GO

CREATE PROC DeleteHorario
@id int
AS
DELETE FROM horarios WHERE id=@id;
GO

-----------boletos--------------
CREATE PROC InsertBoleto
@folio int ,
@idCliente int ,
@idHorario int 
AS
INSERT INTO boletos VALUES(@folio,@idCliente,@idHorario);
GO

CREATE PROC UpdateBoleto
@folio int ,
@idCliente int ,
@idHorario int 
AS
UPDATE boletos SET idCliente=@idCliente,idHorario=@idHorario WHERE folio=@folio;
GO

CREATE PROC ConsultBoleto
@folio int
AS
SELECT * FROM boletos WHERE folio=@folio;
GO

CREATE PROC ConsultBoletos
AS
SELECT * FROM boletos;
GO

CREATE PROC DeleteBoleto
@folio int
AS
DELETE FROM boletos WHERE folio=@folio;
GO

--------ventas_comestibles-------------
CREATE PROC InsertVentaComestibles
@folioVenta int ,
@idCliente int,
@total float
AS
INSERT INTO ventas_comestibles VALUES(@folioVenta,@idCliente,@total);
GO

CREATE PROC UpdateVentaComestibles
@folioVenta int ,
@idCliente int,
@total float
AS
UPDATE ventas_comestibles SET idCliente=@idCliente, total=@total WHERE folioVenta=@folioVenta;
GO

CREATE PROC ConsultVentaComestibles
@folioVenta int
AS 
SELECT * FROM ventas_comestibles WHERE folioVenta=@folioVenta;
GO

CREATE PROC ConsultVentasComestibles
AS
SELECT * FROM ventas_comestibles;
GO

CREATE PROC DeleteVentaComestibles
@folioVenta int
AS
DELETE FROM ventas_comestibles WHERE folioVenta=@folioVenta;
GO

--------detalles_proucto--------------
CREATE PROC InsertDetalleProducto
@idProducto int,
@folioVenta int,
@cantidad int,
@precio float
AS
INSERT INTO detalles_producto VALUES(@idProducto,@folioVenta,@cantidad,@precio);
GO

CREATE PROC UpdateDetalleProducto
@idProducto int,
@folioVenta int,
@cantidad int,
@precio float
AS
UPDATE detalles_producto SET cantidad=@cantidad,precio=@precio WHERE idProducto=@idProducto and folioVenta=@folioVenta;
GO

CREATE PROC ConsultDetalleProducto
@idProducto int,
@folioVenta int
AS
SELECT * FROM detalles_producto WHERE idProducto=idProducto and folioVenta=@folioVenta;
GO

CREATE PROC ConsultDetallesProducto
AS
SELECT * FROM detalles_producto;
GO

CREATE PROC DeleteDetalleProducto
@idProducto int,
@folioVenta int
AS
DELETE FROM detalles_producto WHERE idProducto=@idProducto and folioVenta=@folioVenta;
GO

------------------------------------------------------------------------------------------------

CREATE PROC ConsultIdNamePeliculas
AS
SELECT id,nombre FROM peliculas;
GO

CREATE PROC ConsultIdClientes
AS
SELECT ID FROM clientes;
GO

CREATE PROC ConsultIdHorarios
AS
SELECT id FROM horarios;
GO


--Tests
exec InsertPelicula  @nombre=N'Juanito', @duracion=N'100',@clasificacion=N'AA'
exec ConsultPelicula @id=N'1'
exec DeletePelicula @id=N'1'
exec ConsultPeliculas;


exec InsertCliente @matriculaAuto=N'AA3GFD',@marcaAuto=N'Ford'; 
exec ConsultCliente @id=N'1';
exec ConsultClientes;
exec UpdateCliente @matriculaAuto=N'AAA-BBB',@marcaAuto=N'NISSAN', @id=N'1'
exec DeleteCliente @id=N'1'

exec InsertProductoComestible @nombre=N'Sabritas pequeñas', @precio=N'22.5';
exec UpdateProductoComestible @id=N'2',@nombre=N'Refresco',@precio=N'25';
exec ConsultProductoComestible @id=N'1'
exec ConsultProductosComestibles
exec DeleteProductoComestible @id=N'1'

exec InsertHorario @idPelicula=N'2', @fecha=N'2020-05-20',@hora=N'11:23:23'
exec UpdateHorario @id=N'2',@idPelicula=N'2',@fecha=N'2020-06-21',@hora=N'00:00:00'
exec ConsultHorario @id=N'1'
exec ConsultHorarios
exec DeleteHorario @id=N'2'

exec InsertBoleto @folio=N'22674',@idCliente=N'2',@idHorario='3'
exec UpdateBoleto @folio=N'22673',@idCliente='2',@idHorario='4'
exec ConsultBoleto @folio=N'22673'
exec ConsultBoletos
exec DeleteBoleto @folio=N'22673'

exec InsertVentaComestibles @folioVenta=N'22457',@idCliente=N'2',@total=N'255.54'
exec UpdateVentaComestibles @folioVenta=N'22456',@idCliente=N'3',@total=N'300.00'
exec ConsultVentaComestibles @folioVenta=N'22456'
exec ConsultVentasComestibles
exec DeleteVentaComestibles @folioVenta=N'22457'

exec InsertDetalleProducto @idProducto=N'3',@folioVenta=N'22456',@cantidad=N'5',@precio=N'300'
exec UpdateDetalleProducto @idProducto=N'2',@folioVenta=N'22456',@cantidad=N'10',@precio=N'600'
exec ConsultDetalleProducto @idProducto=N'2',@folioVenta=N'22456' 
exec ConsultDetallesProducto
exec DeleteDetalleProducto @idProducto=N'3',@folioVenta=N'22456'


exec ConsultIdNamePeliculas;
select * from horarios;

