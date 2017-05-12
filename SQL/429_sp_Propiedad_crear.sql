--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Propiedades_crear $$
CREATE PROCEDURE sp_Propiedades_crear (
		inNombrePropiedad 	VARCHAR(255),
		inValorPropiedad 	VARCHAR(255),
		inItem_Id			BIGINT
		)
BEGIN
	INSERT INTO Propiedad 
		(NombrePropiedad, ValorPropiedad, Item_Id) 
	VALUES 
		(inNombrePropiedad, inValorPropiedad, inItem_Id);
END $$