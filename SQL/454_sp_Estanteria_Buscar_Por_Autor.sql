--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Estanteria_Buscar_Por_Autor $$

CREATE PROCEDURE sp_Estanteria_Buscar_Por_Autor (
	inTermino varchar(255),
	inPage int,
	inCantResult int
)
BEGIN
	SELECT DISTINCT
		i.Id as Item_Id,
		i.Titulo as Item_Titulo,
		i.Resumen as Item_Resumen,
		i.VecesVisto as Item_VecesVisto,
		i.Perfil_Id as Perfil_Id,
		i.UrlImagen  AS Item_UrlImagen,
		p.Nombre as Perfil_Nombre
	FROM
		Autor AS a,
		Item AS i,
		Perfil AS p,
		Autor_Item
	WHERE
		Autor_Item.Autor_Id = a.Id 
		AND 
		Autor_Item.Item_Id = i.Id
		AND
		i.Disponibilidad = 1
		AND
		i.Perfil_Id = p.Id
		AND
		(a.Nombre LIKE CONCAT('%', SUBSTRING_INDEX(SUBSTRING_INDEX( inTermino , ' ', 2 ),' ',1) , '%') 
		or
	    a.Nombre LIKE CONCAT('%', SUBSTRING_INDEX(SUBSTRING_INDEX( inTermino, ' ', -1 ),' ',2) , '%'))
	LIMIT inPage, inCantResult ;
END
$$