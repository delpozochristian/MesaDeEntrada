INSERT INTO Estados (Nombre) VALUES ('Cancelado');

ALTER TABLE dbo.Pedidos ADD CanceladoPor int NULL, FechaCancelacion DATETIME NULL; 

ALTER TABLE dbo.Pedidos  WITH CHECK ADD  CONSTRAINT FK_Cancelacion FOREIGN KEY(CanceladoPor)
REFERENCES dbo.Usuarios (id);
