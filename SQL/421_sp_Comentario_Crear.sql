--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Comentario_Crear $$

CREATE PROCEDURE sp_Comentario_Crear (
	inTexto TEXT,
	inFechaCreacion DATETIME,
	inPrestamo_Id BIGINT,
    inPerfil_Id 	BIGINT,
	OUT outId BIGINT
)
BEGIN
	INSERT INTO Comentario
		(Texto, FechaCreacion, Prestamo_Id, Perfil_Id)
	VALUES
		(inTexto, inFechaCreacion, inPrestamo_Id, inPerfil_Id);

	SET outId = LAST_INSERT_ID();
END
$$ 
