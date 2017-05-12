--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Propiedades_seleccionar $$
CREATE PROCEDURE sp_Propiedades_seleccionar (
	inItem_Id BIGINT
	)
BEGIN
	SELECT 
		NombrePropiedad, 
		ValorPropiedad, 
	    Item_Id 
	FROM Propiedad
	WHERE
	Item_Id = inItem_Id;

END $$
