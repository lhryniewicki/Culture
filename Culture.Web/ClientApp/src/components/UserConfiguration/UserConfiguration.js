import React from 'react';
import '../UserConfiguration/UserConfiguration.css';
import { updateUserConfig } from '../../api/AccountApi';


class UserConfiguration extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            commentsAmount: this.props.commentsAmount,
            eventsAmount: this.props.eventsAmount,
            anonymous: this.props.anonymous,
            calendar: this.props.calendar,
            emailNotification: this.props.emailNotification,
            logOutMinutes: this.props.logOutMinutes
        };

    }

    handleConfigSubmit = async (e) => {
        e.preventDefault();
        console.log(this.state);
        await updateUserConfig({
            commentsDisplayAmount: this.state.commentsAmount,
            eventsDisplayAmount: this.state.eventsAmount,
            logOutAfter: this.state.logOutMinutes,
            anonymous: this.state.anonymous,
            sendEmailNotification: this.state.emailNotification,
            calendarPastEvents: this.state.calendar
        });
    }

    handleInputChange = (e) => {
        if (e.target.value === "true") {

            this.setState({ [e.target.name]: true });
        }
        else if (e.target.value === "false") {
            this.setState({ [e.target.name]: false });
        }
        else {
            this.setState({ [e.target.name]: e.target.value });
        }
    } 

    render() {
        return (
            <form className="form-inline" onSubmit={this.handleConfigSubmit}>
            <table className="table ">
                <thead>
                    <tr className="tableHeader">
                        <th scope="col">Opcja</th>
                        <th scope="col">Wartość</th>
                        <th scope="col">Opis</th>
                    </tr>
                </thead>
                    <tbody>
                        <tr>
                            <th scope="row">Anonimowość</th>
                            <td>
                                <fieldset >
                                    <label>
                                        <input checked={this.state.anonymous === true} onChange={this.handleInputChange} type="radio" value={true} name="anonymous" />
                                        Tak
                                 </label>
                                    <label>
                                        <input checked={this.state.anonymous === false} onChange={this.handleInputChange} type="radio" value={false} name="anonymous" />
                                        Nie
                                 </label>
                                </fieldset>
                            </td>
                            <td>Czy chcesz, aby inni użytkownicy mogli widzieć twoje dane?</td>
                        </tr>
                        <tr>
                            <th scope="row">Powiadomienia email</th>
                            <td><fieldset id="group2">
                                <label>
                                    <input checked={this.state.emailNotification === true} onChange={this.handleInputChange} type="radio" value={true} name="emailNotification" />
                                    Tak
                                 </label>
                                <label>
                                    <input checked={this.state.emailNotification === false} onChange={this.handleInputChange} type="radio" value={false} name="emailNotification" />
                                    Nie
                                 </label>
                            </fieldset></td>
                            <td>Czy chcesz, aby notyfikacje przychodziły na twój adres email.</td>
                        </tr>
                        <tr>
                            <th scope="row">Ubiegłe wydarzenia</th>
                            <td> <fieldset >
                                <label>
                                    <input checked={this.state.calendar === true} onChange={this.handleInputChange} type="radio" value={true} name="calendar" />
                                    Tak
                                 </label>
                                <label>
                                    <input checked={this.state.calendar === false} onChange={this.handleInputChange} type="radio" value={false} name="calendar" />
                                    Nie
                                 </label>
                            </fieldset></td>
                            <td>Czy chcesz, aby w kalendarzu znajdowały się wydarzenia, które już się odbyły.</td>
                        </tr>

                        <tr>
                            <th scope="row">Wyloguj po</th>
                            <td>
                                <input type="number"
                                    name="logOutMinutes"
                                    value={this.state.logOutMinutes}
                                    onChange={this.handleInputChange}
                                    className="form-control"
                                    required />
                            </td>
                            <td>Podaj czas w minutach po którym chcesz, aby twoje konto zostało wylogowane (bezpieczeństwo).</td>
                        </tr>
                    <tr>
                        <th scope="row">Ilość komentarzy</th>
                        <td>
                            <input type="number"
                                name="commentsAmount"
                                value={this.state.commentsAmount}
                                onChange={this.handleInputChange}
                                className="form-control"
                                required />
                        </td>
                        <td>Ilość ładowanych komentarzy przy wciśnięciu opcji "Załaduj więcej".</td>
                    </tr>

                    <tr>
                        <th scope="row">Ilość wydarzeń</th>
                        <td>
                            <input type="number"
                                name="eventsAmount"
                                value={this.state.eventsAmount}
                                onChange={this.handleInputChange}
                                className="form-control"
                                required />
                        </td>
                        <td>Ilość ładowanych wydarzeń przy wciśnięciu opcji "Załaduj więcej".</td>
                    </tr>
                    
                   
                </tbody>
            </table>
            <button className="btn btn-primary col-md-offset-2 mb-5">Zapisz</button>
           </form >
        );
    }
}
export default UserConfiguration;



