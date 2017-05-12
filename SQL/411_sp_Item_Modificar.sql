--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Modificar $$

CREATE PROCEDURE sp_Item_Modificar(
	inId BIGINT UNSIGNED,
	inTitulo VARCHAR(256),
	inResumen TEXT,
    inVecesVisto BIGINT UNSIGNED,
    inPerfil_Id BIGINT UNSIGNED
)
BEGIN
	UPDATE Item 
	SET
	 	Titulo 				= inTitulo,
		Resumen 			= inResumen,
	    VecesVisto 			= inVecesVisto,
	    Perfil_Id 			= inPerfil_Id
	WHERE
		Id = inId;
END
$$
