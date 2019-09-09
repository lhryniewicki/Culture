import React from 'react';


class Login extends React.Component {


    render() {
        return (
            <div className="container">
                <form className="text-center border border-gray col-xs-4 col-xs-offset-4 " style={{ marginTop: '20%' }} onSubmit>

                    <p className="h4 mb-4">Logowanie</p>

                    <input type="email" id="defaultLoginFormEmail" className="form-control mb-4" placeholder="Nazwa użytkownika" required />

                    <input type="password" id="defaultLoginFormPassword" className="form-control mb-4" placeholder="Hasło" required />
                    <button className="btn btn-info btn-block my-4" type="submit">Zaloguj</button>
                    <hr />
                    <div className="d-flex justify-content-around">

                        <p>Zapomniałeś hasła?{` `}
                            <a href="">Przypomnij</a>
                        </p>

                        <p>Nie masz konta?{` `}
                            <a href="">Załóż</a>
                        </p>
                    </div>



                   


                </form>
            </div>

        );
    }
}
export default Login;



