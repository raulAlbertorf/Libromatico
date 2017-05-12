--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Propiedades_eliminar $$
CREATE PROCEDURE sp_Propiedades_eliminar(
	inNombrePropiedad 	VARCHAR(255),
	inItem_Id			BIGINT
	)
BEGIN
	DELETE FROM Propiedad
	  WHERE
		NombrePropiedad = inNombrePropiedad AND
	    Item_Id = inItem_Id;
END $$