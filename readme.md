- Alternativsas para configurar a identidade em um projeto ASP.NET Core:
(nesse projet foi utilizado o AddIdentityCore<User>() configurado no Program.cs)

1. AddDefaultIdentity:
Inclui UI Padr�o: AddDefaultIdentity � mais completo, configurando tanto a parte de backend quanto a interface de usu�rio (UI) para gerenciar usu�rios, autentica��o e autoriza��o.
Uso: Ideal para aplica��es web que precisam de uma UI pronta para uso sem a necessidade de criar endpoints de API personalizados.

2. AddIdentityCore:
Minimalista e Flex�vel: AddIdentityCore oferece um controle ainda maior e � mais b�sico. Ele � �til quando voc� precisa de uma solu��o de autentica��o sem UI e quer configurar os detalhes manualmente, incluindo como os endpoints s�o expostos.
Uso: Ideal para APIs ou microservi�os que n�o precisam de uma interface de usu�rio e onde voc� quer personalizar profundamente o funcionamento do Identity.

3. AddIdentityApiEndpoints:
Foco em APIs com Endpoints Padr�o: Combina a simplicidade de AddIdentityCore com a conveni�ncia de fornecer endpoints padr�o para funcionalidades do Identity, tornando-o ideal para APIs que precisam oferecer autentica��o e gerenciamento de usu�rios de forma r�pida e eficiente.
Uso: Perfeito para quando voc� precisa de uma API que lida com autentica��o e gerenciamento de usu�rios, sem a necessidade de desenvolver endpoints personalizados ou utilizar uma interface de usu�rio.#   A u t h I d e n t i t y E x e r c i s e 
 
 #   A u t h I d e n t i t y E x e r c i s e 
 
 