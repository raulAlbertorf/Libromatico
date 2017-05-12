--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Modificar $$
CREATE PROCEDURE sp_Autor_Modificar (
	inId BIGINT,
	inNombre VARCHAR(255)
)
BEGIN
	UPDATE Autor
	SET
		Nombre = inNombre
	WHERE
		Id = inId;
END
$$