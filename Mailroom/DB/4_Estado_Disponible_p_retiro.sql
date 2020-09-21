UPDATE Estados
set Nombre = 'Disponible para retiro'
where Id = 3;

ALTER TABLE dbo.Pedidos ADD AutorizadoRetirar VARCHAR(50) NULL;