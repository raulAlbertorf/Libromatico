--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Prestamo_Modificar $$

CREATE PROCEDURE sp_Prestamo_Modificar(
	inId BIGINT,
	inEstado INT,
	inFechaEnvio DATETIME,
	inFechaRecepcion DATETIME,
	inFechaExpiracion DATETIME,
	inCalificacionalReceptor INT,
	inCalificacionalPrestamista INT,
	inItem_Id BIGINT UNSIGNED,
    inPrestamista_Id BIGINT UNSIGNED,
    inReceptor_Id BIGINT UNSIGNED,
    OUT outUltimaModificacion DATETIME
)
BEGIN
	UPDATE 
		Prestamo 
	SET
		Estado = inEstado,
		FechaEnvio = inFechaEnvio,
		FechaRecepcion = inFechaRecepcion,
		FechaExpiracion = inFechaExpiracion,
		CalificacionAlReceptor = inCalificacionalReceptor,
		CalificacionAlPrestamista = inCalificacionalPrestamista,
		Item_Id = inItem_Id,
		Prestamista_Id = inPrestamista_Id,
		Receptor_Id = inReceptor_Id,
		UltimaModificacion = CURRENT_TIMESTAMP
	WHERE
		Id = inId;

	SET outUltimaModificacion = CURRENT_TIMESTAMP;
END
$$