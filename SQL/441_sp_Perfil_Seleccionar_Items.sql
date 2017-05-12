--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Perfil_Seleccionar_Items $$
CREATE PROCEDURE sp_Perfil_Seleccionar_Items (
	inId BIGINT
)
BEGIN
	SELECT 	
		Id AS Item_Id,
		Titulo,
	    Disponibilidad,
		Resumen,
	    VecesVisto,
	    UrlImagen
	FROM 
		Item
	WHERE 
		Perfil_Id = inId
	ORDER BY Item_Id DESC;
END $$
