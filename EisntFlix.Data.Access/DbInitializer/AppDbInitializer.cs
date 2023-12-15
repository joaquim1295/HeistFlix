using EisntFlix.Data.Access.DbContext;
using EisntFlix.Data.Access.Static;
using EisntFlix.Models;
using EisntFlix.Root.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EisntFlix.Data.Access.DbInitializer
{
    public class AppDbInitializer 
    {


        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder is null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                context.Database.GetPendingMigrations().Any();
                context.Database.Migrate();
                context.Database.EnsureCreated();

                //Streaming
                if (!context.Streamings.Any())
                {
                    context.Streamings.AddRange(new List<Streaming>()
                    {
                        new Streaming()
                        {
                            Name = "HBO Max",
                            Logo = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBw0NDQ8NDQ8PDg0NDxANDQ4PDw8NDQ4PFRUWFxUVFRUZHSghGBslGxYTIj0hJSksMC4uFyEzODMtNygtLisBCgoKDg0OFxAQFy4eIB0tKzAtKy0rLS0rKy0rLS0rLS0tLS0tLS0tLSstLS0tLS0rLSstLS0tLSsrLSstLS0tLf/AABEIAOEA4QMBIgACEQEDEQH/xAAcAAACAgMBAQAAAAAAAAAAAAAAAgEDBQYHCAT/xABGEAABAwICBgUFDQcFAQEAAAABAAIDBBEFEgYHEyExQSJRYXGBFDJ0kbIVIyQ0NUJUc5OhsbPBM1JicoLR0iVDouHwklP/xAAaAQADAQEBAQAAAAAAAAAAAAABAgMABQQG/8QAOhEAAgECAwMJBgQGAwAAAAAAAAECAxEEITEFEmETMkFRcYGhsfAUIjORwdE0coLhFSNCUrLxNVOi/9oADAMBAAIRAxEAPwBFCEL7o7xBUFSkKxhSlcmKQomYhSFOUhRFEcq3KxyqciARyrKsKrKYDKnJHJnJXIilZSOTlIViYhSlMUpRFZBSqSoWFFQhCwrIKhSoWFAIQhAUEIUrAbBCELAN3QoRdROsBSlBSomApCpKQlYzIKRykpSiKxHKtydyrcmQBHKop3KpyIrFKrcmKRyIrFKQpikKwgpSlMUpRFYFKpKVYQhClKsLclCELCioTJUBQUqELAJQoQsY3W6LpLoupHWJJUXUXUErGAlIVJKQlEBDikJTOKrJRAK4qtxTuKpcUUgCuKRxUuKrJREIKrcmJVZKIAKrKklKURCClKklQjYRkFKmSoCgoUqFhCVCFKAoIQoWFJQoQsAlChCxjb7qLpbqLqZ1xrqCUpKi6wBiUhKCUhKwCSVWSth0X0YfiYlLJWw7EsBzML82a/URbgs4dV830uP7J3+S81THYelJwnOzXB/YlKtCLs2c+cVS4rL6S4O7D6l1O6QSloY/OGlgOYX4XKyWi2hcmJwPnZOyEMkMWV0ZeSQAb3Dh1qssTSjTVVy919PaF1IqO83kak4pCV0c6qJvpkX2L/8AJYTA9BZa2ashbUsjNHMYHOMbniQguFwMwt5v3qcdoYaUXJTyWuT7Oony0Os08lISuknVHP8ATYvsH/5oOqKf6bF9g/8AzS/xPCf9nhL7C8vT6zmZKUldIm1RVQaSyrie7k10b4wfG5t6louOYLVYfLsqqIsed7T50cjetjuDh+HNWo4uhWdqc031dPiBTjLRmPQULO6O6H4hiXSgiyw3sZ5CY4e3KeJ8AVepUhTjvTdl1sEmlmzAqF1ej1PC3witdm6oYgGjxcTdTV6nmZfeK14dyEsLS3/iQvB/FsJe2/4P7EXVj1nJkLZNI9CcQw4F8sW0gHGohvJG0dbxa7O8i3atcXtp1YVY70HdcA3T0BChCcQEITIAIUqFKwtyEJlCBrmzXRdJdRdA7BZdKSkLlBKNjDEpSUpKUlYB0vVCejWfzQ/g9dEXOdT56NZ/NF+D10ZfIbT/ABdTu8kcrEfEZxbWkf8AVX/VQ/gtu1QfEJvSXewxatrLw+olxOR0cE0jTFCA6OGSRt7b94FlteqenlioZmyxyRONS4hsjHRuIyM32cBuXRxU4vZsEmr+6WqP+RHuN5WkaAH4bjXpzvblW7rRtX3x3G/T3e3KuVQ+BW7I/wCSPLHmy7vNG8pNo3rHrCdeadL7e6ldw+NT937QqmAwPtcpR3t2yvpf7DUqe+7XPSqwulWARYlSSU8gGexdBJbfFKB0XD8D1gla1qb8p9zn7XNsdt8Gz38zKM2X+HNf710BQrQlh67jGWcHk0LJOErJ6HEtXmghq5nz1rSKamldFsuG3lYbOaf4ARY9fDrXaIomsaGMAa1oDWtaAGtA4ABEMTWDKxoa27nWAsLuJc4+JJPisVpRjsOGUr6mUF2/JFGPOlkPmtH9+QCpicRVxlVZa5JL6GlJzkZpC59TaPYviTdtiVdNRsf0o6OjJiMbTwDz19hv3jgpqNFcUoBt8LxGedzOk6krHbWOYcwDyPgO8Iez077rqq/fbsvbx04gsus35zQQQRcHcQd4IXG9Z2g7aW9fRNywOI28IG6JxNg5vU0m27kezh0fRHSJmJ05kyGKeF5hqYT50Uo4ju/9yWYrKaOeKSGVofHKx0cjTwc1wsR6ith69TB1tNMmutes0ZXizyypX2Yzh7qSqnpXbzBK+K/7wB6J8RY+K+RfZxkpJNaMo2ClChEUlQpUoAbFQmQtcxncyjMkui6J2hrqCUpKglYAxKQlQSqyVjHUNTh6Nb/ND+D10hc11Mno1v8AND+D10pfH7U/F1O7yRysR8Rghc+0x0/mw2sdTMp45WtZG7O6RzD0he1gCs5oNpG/FKaSd8TYSyYxBrHF4IDWm9yO1Rng60KSrNe67dPWI6UlHeehsq0XV98fxv05/tyLelomr34/jfp7/bkT0PgVuyPmCOkvXSje187qSIkkxsJO8ktaSSvoXGsf1nYlTVtTTxspyyGeWJmaN5dla4gXIdxSYbCVMTJxp2yzzZoQctDsbWgCwFgOAG4LAYrjc8FfSUjaZ+xqZMr6pxaYhZjnZWgG+bo/Ot2XXyavtKXYtSvkkY1k0EmykDL7N1wCHNvvHd2Lai0HiL77+KSUHRqOFSOaurfXL5oVrddmhloWmDRPjuDU8u+EGafIeD5GgFu7nbKPvW+rUNYGDTzxwVtEL12HSbaFvHaMNs7Lc72G7nYjmnwkkqqu7XUlfqbi0vFhi7M29C1rRzTKhxCPoythqB0ZKaZwjmY8cQAbZh2j7lbj2ltBQRl8s7HSW6EEbmvnkPIBo4d53KXI1VPk9x73VbMFnexg8IYINKK6OPcyoo46iRo80SgtF7dfE/1Fb4tK0BwyodJVYtWtMdViDhkiNwYaZtgxpB4EgN8Gjndbqq4trlEr33VFN8UkvDRcEZnANacYbjNTb5wic7v2bR+gWqLM6aV4q8Uq52m7HSlsZ5FjAGAjvy38VhwvrsMnGjBPWy8kOFkKQEWVhbhZFlKEBbkWQpQia5k7ouqrounO4WEqCVXmSkrGHJSEoJVZKIDqmpY9Gt/mh/B66YuVanauKJtZtZI47uhtne1l9zuF10j3WpfpEH20f918dtRP2up3eSOViPiM4zrbP+rP+qh/ArctS/yfN6U78ti0fWrOyTFXuje17djCMzHBzeB5hbfqerYYsPmEsscZNU4gPe1hIyM32JXTxX/GQ/SXn8CPcdKWh6vPj+Oenu9uVbf7rUn0mD7aP+60fQGvgZXY0XzRMD65zmF0jGhwzy7xc7xwXIofArdkf8keWOkvXSdFXPsU1V0VVUTVD6ipa6eR8rmt2OUFxuQLt4LdPdak+kwfbR/3R7rUn0mD7aP+6jRr1aLbptq4IycdD4dF9HafC4DT0+YhzjJJJIQZJHndc2AHAAWHUstNK2NjnvIaxgLnOO4NaBckrG12kuHQNzS1lMwfXMLj3AG58FyXWFrF8uY6jog5lK622mddskw/dDfms7954bhxvQwtfF1L556yYVGU2b3oDptFiZmgkIbURyyviadxkpi4lhHa0EA9wPNbsvJ9JUyQyMmhe5krHB8b2mzmuHMLr2imtmF7WxYm0xSbh5RG0uif2vaN7T3XHcvfj9kyg9+grrq6V9105DTp2zRuWN6HYZXuMlRTNdKeMrS6KQ95aRfxVeD6FYVRPEkFKzaN3tkkLpntPWMxNj2hZCj0goKhuaGrp5B/DMy47xe4U1ePUMLc81XTxt63TRj9d65XK4hR5Lelb+27t8v2J3ehk1pOsrSptBSup4nfDKlhawAjNFGdxkPVzA7e4rFaT61YI2uiw5u3l3jbSAsgZ2hp3v8AuHauTVtZLUSvnne58jzme9xuSf7di6WB2XOUlOsrRXR0v7IyRUAgBACYBfSNmbABSmARZARkWRZPZFkAXEshPZQiC5fdRdJdF1Q745KUlKSlJse5FK5j7MPoZ6qVsFPG6WR/BjRy5kngB2lb9hmqeVwDqupbET/twtMxHe91h9y27V/o4ygo2Oc0eU1DWyTuPnC4u2O/U2/ruvs0j0sosMDRUvJkfvZDG3PK4dduAHaSF85idqVqlXk8Mvkrt8eC6jwVMRKUt2BqU+qOAj3usla7lnije31Cy0rSfQavw5plcGzUw4zRZnBg/jad7fvHaui0OtPDJZAyQTwAm20kZGYx3ljiR32st2GSRnzXxyN7HNe0j1EEKa2jjsLJKte3U1a/Y0Ly9aD97xOE6M6va7EGCZ2WmgdvbJKDneOtjBvI7SR4rbo9T1NbpVk5dzLY42j1G/4ro9RURwxukkc2OKNpc97iGsY0cyeQWiVetvDI3lscdVM0f7jI2Mae4PcD6wEFtDH4mTdFPL+1adrBy1WbvE13GdUVQxpfRVLZiN+zlbsXnucLgnvsub11JNTSuimY+KSM2ex4s4f9dq9IaN6U0WJsc6lku5ltpE8ZJY78Lt5jtFx2rX9a2jLKyhfVMaPKqNhlDhuL4W3L2HrsLkdo7SvRhNrV4VVSxPXa7yafHh1hhXknaZwS56z61m9HNFa/E3FtLGSxps+aQlkLD1F1t57ACVboTo2/Fa5lMLtiaNrPIOLIgRe3aTYDvvyXozDqGGliZBAxsUUYysY0WAH6ntXu2ltR4Z8nDOT+S/cerW3clqcwoNTLcoNTWvLubYoxlH9Tib+oKyq1Mw5feK2RruW1iY9pP9JFlsOOazMJo3uizyVMjDle2naHNaeYL3ENPgSvnwvWthVQ8MeZqYuIAdMxuzv2uY51h2mwXJ9o2pJb/vW/KvKxDeqPM5dpDoHiWHuG0j28T3NYyWC8jC5xs1pFgWkm3EW38VsjNUNS2nM0tVGyVsbpDE1jntBAvlz3HrsuzMe17Q5pDmuAc1wILSOIIKpxP4vN9VJ7JU57ZxMopKya1dln87+GovKNnENEtXrMVpW1Mdc1rrmOWMwl7o3jkTm37rG/arNJNWFVQQOqYZW1DIhmlY2MxStZzcBc5gOfDd1rDavtJ3YXVte4k002WOpYN/R5PA623PgSF6IjeyRgc0tfHI0OaRZzXtI3EdYIXux+MxeFr868Hpku9Zeuk0pO55aCkBbZrF0VOG1ZfEPglSS+A8o3cXReHEdncVj9D9H5MTrGQNu2IdOeQf7cQO+3aeA/6K6kcTTlS5a/u2v649H+xGzK6I6BVOJRmfaNp4LlrHuaXvkI45W7t3bdZTGtWraGmlqpq5gZE0m2xILnfNaOnxJsF16kpo4I2QxNDI42hjGNFg1oFgFxjWZpP5bU+TQuvS0ziLjzZZhuc7tA3geJ5ri4bGYnFV7Re7HV5LJd61frQV6Gv1eG00dO2VlZHLMRGTThjg9uYAu6V7dEkjwWLATAIAXbimlm7+uBNsLKbKbIsixbi2UprIWBvFN1F0l0XXpPoxrr6sJYJKmnY7e108LHDrBe0EL4rr7cCPwyl9Jg/MalnzX2PyFbsj02vNumFa+oxKrfIST5RNG3+FjHFjQO4AL0kvMekh/1Cs9LqfzXL5zYKXKTfBeZ4cJq+wx5K7lqfrXzYTleSfJ53wMJ39DIx4HcM9vBcLcV2rUj8mz+mv8AyYF79tJey/qX1K4nmd4muyufHQQwtJDZ5/fLfOaxpNj4lp8FxIldi17fsKL62X2WrjZVNjRSwkeLYMPzDadWdZJDjNJkNhK8xSDk5j2m4PjY94C9D1LA6N7TvDmuaR2EWXm/V78sUP17f1XpOTge4rk7dX8+H5fqyGJ5yOd6lMMEVFUVJHTnqHRh3MxRiw/5GRW64tIpKKiZTQuLJq0vaXNNnNhaBmseRJc0dxKyeqq3uPDb/wDWov37V60PXzfyuj/d2Drd+c3/AEQpRVfact/P3n/5WXkhdamZzAKWpUwX1TZds6/qS0he4S4bK4ubGzb01zctbcB7B2XLSO8rqOJ/F5vqpPZK4JqgJ924LcCyozfy7M/rlXe8T+LzfVSeyV8ftilGGJbj/Uk+/p+drnlnqeVouA8F17VDpVdowuodvaC6ic4+c0b3Rd43kdl+pcij4DwX10Ukkckb4S4TNe10Zb520uMtus3tuX0mNw0cRCUH3PqfrXgaR6S0hwWDEaV9LODkfYtc2wfG8cHNJ5hfLorozT4VC6KAue6R2eWWS2d54AbhYADl39ay1C6QwxGYBspjYZQOAfYZgPG6+hfF8rPcdPe9297cRDRdZ2lHkVP5LA61VUtNyD0oYeBd2E7wO4nkuMtCyek0tRJX1Lqq+32z2vB+aAbNA7LWt2LHAL6vBYdUKSitXm366F0EZSIATAKQEwC9RO5ACmyayLIC3IshPZCxrmLui6S6Lr1n0wy+7AT8NpvSYPzGrHXX34AfhtL6TB+Y1LU5kux+Qsnkz0+vMOkh+H1npdR+Y5enl5f0k+UKv0uo/Mcvndg8+p2I8OE1ZjiV2zUf8mVHpr/yYFxEldu1HfJlR6a/8mBe/bX4X9S+pXE8w+LXv+wovrZfZauNldj17/sKL6yX2Wrjapsf8JHtfmChzEbDq9+WaH69v6r0o/zT3FeatXvyzQ/Xt/VelZPNPcVydu/Gh+X6shiOcjnupjEBJQ1FMSM9NVPNuezk3g//AEJPUk1z6PvqqOOshaXPoi8yNHEwPtmPblLWnuuua6D6THCsR2zrmnlJiqGjedmX7nAcy07/AFjmvRNLUxTxNlic2SKVocx7SHMe08wkx0Z4PGKvFZPNcetefzFn7srnk0Jgu8Y7qqw2qeZITJSOcblsOV0F+xh83uBAVGFaocPheHzyzVIBuIzlhiP82XefWuqttYXdvn2W9IZ1EYfUlgL882JSNtHkNPAT88kgvcOwWaL9d+pdWxP4vN9VJ7JTU0EcLGxRMbHGwBrGMaGsa0cAAOAS4l8Xm+qk9kr5nF4h4irKo8r+CWhFu7PLMPAeH4LqGqPRXaye6dQ33uMltK0/PkG4ydzd4HbfqC0vQzR6TE6uOnbcRi0k8gH7OIWv4ngO/sK9F0VLHTxRwQtDIomNjjYODWgWAXf2xjOTToxecteC/fyM9SrFMQhpIH1FQ8RxRi7nHjv3AAcyTYWVOA43TYhDt6Z2ZgcWOBBa9jhvs4cjYg+K5LrM0o8tqPJYXXpaVxBIPRlmG5zu0DeB4nqWO0G0idhlWHuJ8mmtHUNH7t9zwOttz4Ernx2VJ4ff/r1S4fd/t2Tc8zetaei+3j90IG++wttUNA3yRDg7vbv8D2LlAC9LNc17QQQ5jxcHcWuaR94suJ6eaNe51VeNvwWoJdBbgw/Oj8OXZ3FejZWLuuRl0afb7fISquk1cBMApATgLsHnbFspsmAUpRbi2UJ7KVjXMBdF0t1N17z6kLp4JnRvZIw2fG5r2G17Oabg+sKu6hYB2Gk1xU2zbt6Wba26eydG5l+y5BsuT4rVCeomnALRNLLMGniA9xdY+tfKlXkw+Co4dydNWvxuShSjDTpJK3TV9p37kNlhkidLTSv2vvZaJGSWDSRfcQQ1vqWlEpVavQhXg4VFdMM4qSszetY2m8GLx07IIZItg973GXJvzAAAZSeorRFJULUKEKEFThohIxUVZH04XXyUtRFUxEB8EjZWX3glpvY9h4LrseuWkMYz0k4kLd4a6JzM3YSQbeC4whSxOCo4hp1FpxsTnBS1Ge65J6yT6yti0U00r8KOWF4fATd1PJd0RPW3mw93iCtaTBWqU4VI7s1dAlmdpoNclG5o8opaiN3PZGOZn3lp+5XVeuGgaPeaepkdyzbKJnicxP3LiQTBc17Hwt72fz9PxIOKN9qtamJS1UUzRHDBE/MaZtyJG8CJHHedxPCwB32Nls9RrcpJIns8lqQ57HN4w5QSLcc17eC48ArGhNU2bhpJLctbqy/35iM6DoNpph+E0uy8mqJKiQ5p5G7GzyNzQ27gcoHLtKyGkmtDyinfDRQywvlGR0sjmZmNPHIGk7yN1+S5k0K1oWns+hKpykldt3zeXy+mhJyYzQrGhK0K1oXpZFs3rQ/T80NOKepifPFH+xcxzdoxv7hB4jq37uC+/SLTnD8QpZKaSmqOkM0b/erxyDzXDpf+BK500KxoXhlgaLqcpZp65O2YrqStYAE4CAE4C9JBsgBTZMAmsgxbldkK2yFrmuardF1F1C6J9YTdQoQSsACoKFBWA2CFChEUlQpUIAYICELEmSFIUBSEGTYwTBATBKTYzVY0JGhWNSMjIdoVrQkaFa1IyMhmhWtCRoVrQptkZDtCsaErQrWhKRbABWAKGhWAJLk2wDUWTgKbIC3K7KVZZCxrmmIUKF1D68FCFKBgSoKERSEIQsAFClCAjBCFIWJsEwUKQlJyGCsCQKwJSUh2p2qGpmpGRkWtVjUjVY1IyMixquaFU1XtSMhIZoVrQkaFa0KbJSHaFYAkaFYAkJsAE9kAJrICXEshOha4DRUKELrH2ZKhCEAAoKlQsAhCChEAIUKUBGwTBIpCxNjBMEoThKSkME7UgVjUjJMdqsaq2q1qUjIsarmqpquapsjIdquaqmq5qRkJFjQrmqpqtapslIsanCVicJSTGamCgKUghNkKELGNDUoQuwj7MhCEIGBQhCwCChCEQEKUIQJsFKlCAjAJwhCBJjtTtUoSMkx2qxqlCUjIsarmqEKbISLWq9qEJGRkWNVzUIU2SkWtTBShKRYwUoQkFBCELAP/2Q==",
                            Description = "HBO Max is a stand-alone streaming platform that bundles all of HBO."
                        },
                        new Streaming()
                        {
                            Name = "NetFlix",
                            Logo = "https://rosenfeldmedia.com/designopssummit2018/wp-content/uploads/sites/10/2018/09/netflix-logo-square.png",
                            Description = "Netflix is a streaming service."
                        }
                    });
                    context.SaveChanges();
                }
                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-1.jpeg"

                        },
                        new Actor()
                        {
                            FullName = "Actor 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-4.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Producer 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-1.jpeg"

                        },
                        new Producer()
                        {
                            FullName = "Producer 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-2.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-3.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Enter the Void",
                            Description = "This psychedelic tour of life after death is seen entirely from the point of view of Oscar, a young American drug dealer and addict living in Tokyo with his prostitute sister, Linda.",
                            Price = 1.00,
                            ImageURL = "https://image.tmdb.org/t/p/w500/krKnsfvSJM1PL40tLicRhVQ6kuG.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            Rating = 9,
                            StreamingId = 2,
                            ProducerId = 3,
                            FilmCategory = FilmCategory.Drama
                        },
                        new Movie()
                        {
                            Name = "Naked Lunch",
                            Description = "Blank-faced bug killer Bill Lee and his dead-eyed wife, Joan, like to get high on Bill's pest poisons while lounging with Beat poet pals. ",
                            Price = 2.00,
                            ImageURL = "https://image.tmdb.org/t/p/w500/pOWDc5E2XpV8kEafun4htl70RYh.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            Rating = 8,
                            StreamingId = 2,
                            ProducerId = 1,
                            FilmCategory = FilmCategory.Thriller
                        },
                        new Movie()
                        {
                            Name = "Brazil",
                            Description = "Low-level bureaucrat Sam Lowry escapes the monotony of his day-to-day life through a recurring daydream of himself as a virtuous hero saving a beautiful damsel. ",
                            Price = 19.50,
                            ImageURL = "https://cdn.discordapp.com/attachments/1038485490908811277/1059876341756997633/8Pf3pbzu9pyONdpmdMtMrASr0am.png",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            StreamingId = 2, 
                            ProducerId = 4,
                            FilmCategory = FilmCategory.Comedy
                        },
                        new Movie()
                        {
                            Name = "Fear and Loathing in Las Vegas",
                            Description = "Raoul Duke and his attorney Dr. Gonzo drive a red convertible across the Mojave desert to Las Vegas with a suitcase full of drugs to cover a motorcycle race. ",
                            Price = 29.50,
                            ImageURL = "https://cdn.discordapp.com/attachments/1038485490908811277/1059876793689051206/gFo5UrXQaVDQ9Vc1mmsWZNRt2aQ.png",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            Rating = 7,
                            StreamingId = 1,
                            ProducerId = 2,
                            FilmCategory = FilmCategory.Comedy
                        },
                        new Movie()
                        {
                            Name = "House of 1000 Corpses",
                            Description = "Two teenage couples traveling across the backwoods of Texas searching for urban legends of serial killers end up as prisoners of a bizarre and sadistic backwater family of serial killers.",
                            Price = 39.50,
                            ImageURL = "https://cdn.discordapp.com/attachments/1038485490908811277/1059878649085558874/afISpRBO81uaLy5hrMHqyof6C1J.png",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            StreamingId = 1,
                            Rating = 7,
                            ProducerId = 3,
                            FilmCategory = FilmCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Fight Club",
                            Description = "A ticking-time-bomb insomniac and a slippery soap salesman channel primal male aggression into a shocking new form of therapy. ",
                            Price = 39.50,
                            ImageURL = "https://cdn.discordapp.com/attachments/1038485490908811277/1059877208698658826/pB8BM7pdSp6B6Ih7QZ4DrQ3PmJK.png",
                            Rating = 9,
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            StreamingId = 1,
                            ProducerId = 5,
                            FilmCategory = FilmCategory.ActionAdventure
                        }
                    }); ;
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 1
                        },

                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 2
                        },
                         new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 2
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 4
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 5
                        },


                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 6
                        },
                    });
                    context.SaveChanges();
                }
                //Series
                if (!context.Series.Any())
                {
                    context.Series.AddRange(new List<Serie>()
                    {
                        new Serie()
                        {
                            Name = "The Last of Us",
                            Description = "Twenty years after modern civilization has been destroyed, Joel, a hardened survivor, is hired to smuggle Ellie, a 14-year-old girl, out of an oppressive quarantine zone.",
                            Price = 1.00,
                            ImageURL = "https://image.tmdb.org/t/p/w500/uKvVjHNqB5VmOrdxqAt2F7J78ED.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            Rating = 9,
                            StreamingId = 2, 
                            ProducerId = 3,
                            FilmCategory = FilmCategory.Drama
                        },
                        new Serie()
                        {
                            Name = "Game of Thrones",
                            Description = "Seven noble families fight for control of the mythical land of Westeros. Friction between the houses leads to full-scale war.  ",
                            Price = 2.00,
                            ImageURL = "https://image.tmdb.org/t/p/w500/7WUHnWGx5OO145IRxPDUkQSh4C7.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            StreamingId = 2, 
                            ProducerId = 1,
                            FilmCategory = FilmCategory.Thriller
                        },
                        new Serie()
                        {
                            Name = "Breaking Bad",
                            Description = "When Walter White, a New Mexico chemistry teacher, is diagnosed with Stage III cancer and given a prognosis of only two years left to live. He becomes filled with a sense of fearlessness and an unrelenting desire to secure his family's financial future at any cost as he enters the dangerous world of drugs and crime. ",
                            Price = 19.50,
                            ImageURL = "https://image.tmdb.org/t/p/w500/ggFHVNu6YYI5L9pCfOacjizRGt.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            StreamingId = 2, 
                            Rating = 6,
                            ProducerId = 4,
                            FilmCategory = FilmCategory.Comedy
                        },
                        new Serie()
                        {
                            Name = "Peaky Blinders",
                            Description = "A gangster family epic set in 1919 Birmingham, England and centered on a gang who sew razor blades in the peaks of their caps, and their fierce boss Tommy Shelby, who means to move up in the world.",
                            Price = 29.50,
                            ImageURL = "https://image.tmdb.org/t/p/w500/vUUqzWa2LnHIVqkaKVlVGkVcZIW.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            Rating = 7,
                            StreamingId = 1, 
                            ProducerId = 2,
                            FilmCategory = FilmCategory.Comedy
                        },
                        new Serie()
                        {
                            Name = "Sherlock",
                            Description = " modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.",
                            Price = 39.50,
                            ImageURL = "https://image.tmdb.org/t/p/w500/7WTsnHkbA0FaG6R9twfFde0I9hl.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            Rating = 8,
                            StreamingId = 1,
                            ProducerId = 3,
                            FilmCategory = FilmCategory.Horror
                        },
                        new Serie()
                        {
                            Name = "1923",
                            Description = "Follow a new generation of the Dutton family during the early twentieth century when pandemics, historic drought, the end of Prohibition and the Great Depression all plague the mountain west, and the Duttons who call it home.",
                            Price = 39.50,
                            ImageURL = "https://image.tmdb.org/t/p/w500/zgZRJZvZn5cpsWAB0zMUdad3iZd.jpg",
                            Rating = 7,
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            StreamingId = 1,
                            ProducerId = 5,
                            FilmCategory = FilmCategory. ActionAdventure
                        }
                    });
                    context.SaveChanges();
                }
                //Actors & Series
                if (!context.Actors_Series.Any())
                {
                    context.Actors_Series.AddRange(new List<Actor_Serie>()
                    {
                        new Actor_Serie()
                        {
                            ActorId = 1,
                            SerieId = 1
                        },
                        new Actor_Serie()
                        {
                            ActorId = 3,
                            SerieId = 1
                        },

                         new Actor_Serie()
                        {
                            ActorId = 1,
                            SerieId = 2
                        },
                         new Actor_Serie()
                        {
                            ActorId = 4,
                            SerieId = 2
                        },

                        new Actor_Serie()
                        {
                            ActorId = 1,
                            SerieId = 3
                        },
                        new Actor_Serie()
                        {
                            ActorId = 2,
                            SerieId = 3
                        },
                        new Actor_Serie()
                        {
                            ActorId = 5,
                            SerieId = 3
                        },


                        new Actor_Serie()
                        {
                            ActorId = 2,
                            SerieId = 4
                        },
                        new Actor_Serie()
                        {
                            ActorId = 3,
                            SerieId = 4
                        },
                        new Actor_Serie()
                        {
                            ActorId = 4,
                            SerieId = 4
                        },


                        new Actor_Serie()
                        {
                            ActorId = 2,
                            SerieId = 5
                        },
                        new Actor_Serie()
                        {
                            ActorId = 3,
                            SerieId = 5
                        },
                        new Actor_Serie()
                        {
                            ActorId = 4,
                            SerieId = 5
                        },
                        new Actor_Serie()
                        {
                            ActorId = 5,
                            SerieId = 5
                        },


                        new Actor_Serie()
                        {
                            ActorId = 3,
                            SerieId = 6
                        },
                        new Actor_Serie()
                        {
                            ActorId = 4,
                            SerieId = 6
                        },
                        new Actor_Serie()
                        {
                            ActorId = 5,
                            SerieId = 6
                        },
                    });
                    context.SaveChanges();
                }

            }
        }

      public  static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminUserEmail = "admin@eisntflix.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Local Ghost",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Rootcat@1295");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@eisntflix.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "First User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Rootcat@1295");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

       public static async Task Initialize(IApplicationBuilder applicationBuilder)
        {
            Seed(applicationBuilder);
            await SeedUsersAndRolesAsync(applicationBuilder);
        }
    }
}
