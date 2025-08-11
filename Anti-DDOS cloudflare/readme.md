CloudFlareResolver
Bypass da detecção bot-anti-ddos do Cloudflare

Basta fornecer o HTML do anti-ddos do Cloudflare que ele retorna o jschl-answer para fazer a requisição!

Como funciona?
O Cloudflare possui um JavaScript que verifica se você é um bot ou não. Este código lê esse JavaScript entendendo algumas expressões como:

js
Copiar
Editar
!+[] == 1  
!![] == 1  
1+[]+2 == 12  
Com isso, você consegue realizar cálculos matemáticos para obter a resposta correta.

Exemplo do código gerado pelo Cloudflare
js
Copiar
Editar
AFRwsPU.WlNfklkua-=+((!+[]+!![]+!![]+!![]+[])+(+!![]));
AFRwsPU.WlNfklkua+=+((!+[]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]));
AFRwsPU.WlNfklkua-=+((!+[]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]));
AFRwsPU.WlNfklkua+=+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]));
AFRwsPU.WlNfklkua+=+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]));
AFRwsPU.WlNfklkua+=+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]));
O que isso significa?
O código acima é equivalente a:

js
Copiar
Editar
AFRwsPU.WlNfklkua-=+((1+1+1+1+[])+(+1));
AFRwsPU.WlNfklkua+=+((1+1+1+1+[])+(1+1+1+1+1));
AFRwsPU.WlNfklkua-=+((1+1+1+1+[])+(1+1+1+1));
AFRwsPU.WlNfklkua+=+((1+1+[])+(1+1+1+1+1));
AFRwsPU.WlNfklkua+=+((1+1+[])+(1+1+1+1+1+1+1+1));
AFRwsPU.WlNfklkua+=+((1+1+[])+(1+1+1+1+1+1));
E isso é equivalente a números simples:

js
Copiar
Editar
AFRwsPU.WlNfklkua-=41;
AFRwsPU.WlNfklkua+=45;
AFRwsPU.WlNfklkua-=44;
AFRwsPU.WlNfklkua+=25;
AFRwsPU.WlNfklkua+=28;
AFRwsPU.WlNfklkua+=26;

criado por Bernardo Capellini