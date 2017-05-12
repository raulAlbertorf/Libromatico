--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Autor_Buscar $$

CREATE PROCEDURE sp_Autor_Buscar (
	inNombre varchar(255)
)
BEGIN
	SELECT DISTINCT
		Id AS AutorId,
		Nombre
	FROM
		Autor
	WHERE
		
		Nombre LIKE CONCAT('%', SUBSTRING_INDEX(SUBSTRING_INDEX( inNombre , ' ', 2 ),' ',1) , '%') 
		or
	    Nombre LIKE CONCAT('%', SUBSTRING_INDEX(SUBSTRING_INDEX( inNombre , ' ', -1 ),' ',2) , '%');
END
$$