--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Perfil_Seleccionar_Prestamo $$

CREATE PROCEDURE sp_Perfil_Seleccionar_Prestamo(
	inReceptor_Id BIGINT,
	inItem_Id BIGINT
)
BEGIN
	SELECT  
		Id,
		Estado,
		FechaEnvio,
		FechaRecepcion,
		FechaExpiracion,
		CalificacionAlReceptor,
		CalificacionAlPrestamista,
		UltimaModificacion,
		Item_Id,
		Prestamista_Id,
		Receptor_Id
	FROM 
		Prestamo
	WHERE
		Receptor_Id = inReceptor_Id
		AND
		Item_Id = inItem_Id;
END
$$