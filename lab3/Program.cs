using Lab3;

IPaymentService mokejimas = new GryniesiaisMokejimas();
mokejimas.Moket(50m);

IPaymentService mokejimas2 = new KortelesMokejimas();
mokejimas2.Moket(50m);

IPaymentService mokejimas3 = new KriptoMokejimas();
mokejimas3.Moket(50m);
