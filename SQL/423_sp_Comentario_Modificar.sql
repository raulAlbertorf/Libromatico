--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Comentario_Modificar $$

CREATE PROCEDURE sp_Comentario_Modificar (
	inId BIGINT,
	inTexto TEXT,
	inFechaCreacion DATETIME,
	inPrestamo_Id BIGINT,
    inPerfil_Id		BIGINT
)
BEGIN
	UPDATE Comentario
	SET
		Texto		  	= inTexto,
		FechaCreacion 	= inFechaCreacion,
		Prestamo_Id     = inPrestamo_Id,
        Perfil_Id		= inPerfil_Id
	WHERE Id = inId;
END
$$