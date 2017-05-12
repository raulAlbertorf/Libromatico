--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Propiedades_modificar $$
CREATE PROCEDURE sp_Propiedades_modificar(
	inNombrePropiedad 	VARCHAR(255),
	inValorPropiedad 	VARCHAR(255),
	inItem_Id			BIGINT
	)
BEGIN
	UPDATE Propiedad
	SET  
		ValorPropiedad = inValorPropiedad
	WHERE
		Item_Id = inItem_Id AND 
		NombrePropiedad = inNombrePropiedad;
END $$