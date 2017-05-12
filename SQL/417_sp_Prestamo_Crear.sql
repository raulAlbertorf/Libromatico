--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Prestamo_Crear $$

CREATE PROCEDURE sp_Prestamo_Crear(
	inEstado INT,
	inFechaEnvio DATETIME,
	inFechaRecepcion DATETIME,
	inFechaExpiracion DATETIME,
	inCalificacionalReceptor INT,
	inCalificacionalPrestamista INT,
	inItem_Id BIGINT UNSIGNED,
    inPrestamista_Id BIGINT UNSIGNED,
    inReceptor_Id BIGINT UNSIGNED,
    OUT outId BIGINT
)
BEGIN
	INSERT INTO Prestamo 
		(
			Estado,
			FechaEnvio,
			FechaRecepcion,
			FechaExpiracion,
			CalificacionAlReceptor,
			CalificacionAlPrestamista,
			Item_Id,
			Prestamista_Id,
			Receptor_Id
		)
	VALUES
		(	
			inEstado,
			inFechaEnvio,
			inFechaRecepcion,
			inFechaExpiracion,
			inCalificacionalReceptor,
			inCalificacionalPrestamista,
			inItem_Id,
			inPrestamista_Id,
			inReceptor_Id
		);
	SET outId = LAST_INSERT_ID();
END
$$	