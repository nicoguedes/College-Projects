/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.controller;

import gestaoacademica.dominio.Aluno;
import gestaoacademica.persistencia.AlunoDAO;

public class AlunoController {
    private static AlunoController instancia;
    
    public static AlunoController getInstance(){
        if (instancia == null)
            instancia = new AlunoController();
        
        return instancia;
    }
    
    public Aluno get(int codigo) throws Exception
    {
        return AlunoDAO.getInstance().get(codigo);
    }
}
