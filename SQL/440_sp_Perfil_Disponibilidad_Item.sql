--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Perfil_Disponibilidad_Item $$

CREATE PROCEDURE sp_Perfil_Disponibilidad_Item(
	inItem_Id BIGINT UNSIGNED,
	inDisponibilidad BOOLEAN
)
BEGIN
	UPDATE Item 
	SET
		Disponibilidad = inDisponibilidad
	WHERE
		Id = inItem_Id;
END
$$
