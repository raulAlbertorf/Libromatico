--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Eliminar $$
CREATE PROCEDURE sp_Autor_Eliminar (
	inId BIGINT
)
BEGIN
	DELETE FROM Autor
	WHERE 
		Id = inId;
END
$$