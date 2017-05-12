--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Prestamo_Seleccionar $$

CREATE PROCEDURE sp_Prestamo_Seleccionar(
	inId BIGINT
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
		Id = inId;
END
$$