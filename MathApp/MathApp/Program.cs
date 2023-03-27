var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Terminating middleware
app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        int firstNumber = 0, secondNumber = 0;
        string? operation = null;
        long? result = null;

        //read 'firstNumber' if submitted in the query
        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            string firstNumberString = context.Request.Query["firstNumber"][0];

            if (!string.IsNullOrEmpty(firstNumberString))
            {
                //convert firstNumber to int
                firstNumber = Convert.ToInt32(firstNumberString);
            }
            else
            {
                if (context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
                }
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
            }
        }

        //read 'secondNumber' if submitted in the query
        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            string secondNumberString = context.Request.Query["secondNumber"][0];
            if (!string.IsNullOrEmpty(secondNumberString))
            {
                //convert secondNumber to int
                secondNumber = Convert.ToInt32(secondNumberString);
            }
            else
            {
                if (context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
                }
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
            }
        }

        //read 'operation' if submitted in the query
        if (context.Request.Query.ContainsKey("operation"))
        {
            operation = Convert.ToString(context.Request.Query["operation"][0]);

            if (!string.IsNullOrEmpty(operation))
            {
                //perform the calculation based on the value of operation
                switch (operation)
                {
                    case "add": result = firstNumber + secondNumber; break;
                    case "subtract": result = firstNumber - secondNumber; break;
                    case "multiply": result = firstNumber * secondNumber; break;
                    case "divide": result = (secondNumber != 0) ? firstNumber / secondNumber : 0; break;
                    case "mod": result = (secondNumber != 0) ? firstNumber % secondNumber : 0; break;
                }

                //if result has value
                if (result.HasValue)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(result.Value.ToString());
                }
                else
                {
                    if (context.Response.StatusCode == 200)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid input for operation\n");
                    }
                }
            }
            else
            {
                if (context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for operation\n");
                }
            }
        }
    }
});

app.Run();
