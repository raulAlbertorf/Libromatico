--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Comentario_Eliminar $$

CREATE PROCEDURE sp_Comentario_Eliminar (
	inId BIGINT UNSIGNED
)
BEGIN
	DELETE FROM
		Comentario
	WHERE
		Id = inId;
END
$$