import React from 'react';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import { getAllCalendar } from '../../api/EventApi';
import '../AllMap/AllMap.css';
import { Link } from 'react-router-dom';
import { userIsAuthenticated } from '../../utils/JwtUtils';
import { Redirect } from 'react-router';
import CategoriesWidget from '../Widgets/CategoriesWidget';
import SearchWidget from '../Widgets/SearchWidget';
import DayPickerInput from 'react-day-picker/DayPickerInput';
import DayPicker from 'react-day-picker/DayPicker';
import { Modal } from 'react-bootstrap';

class AllCalendar extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            query: null,
            date1: null,
            date2: null,
            displayDate1: null,
            displayDate2: null,
            category: null,
            disabled: true,
            selectedDays: [],
            showModal: false,
            modalDate: "",
            modalEvents: []
        }
    }

    componentDidMount = async () => {
        this.refs.dayPicker.input.readOnly = true;
        this.refs.dayPicker.input.setAttribute("class", "form-control");
        this.refs.dayPicker.input.setAttribute("class", "form-control");
        this.refs.dayPicker.input.style.backgroundColor = "white";
        this.refs.dayPicker1.input.readOnly = true;
        this.refs.dayPicker1.input.setAttribute("class", "form-control");
        this.refs.dayPicker1.input.setAttribute("class", "form-control");
        this.refs.dayPicker1.input.style.backgroundColor = "white";

        let dates;
        if (this.state.date1 !== null) {
            dates = [this.state.date1];

            if (this.state.date2 !== null) dates = dates.concat([this.state.date2]);
            console.log(dates);
        }

        const days = await getAllCalendar(this.state.query, dates, this.state.category);
        const JsDays = days.map((item, key) => {
            return new Date(Date.parse(item));
        })
        this.setState({
            selectedDays: JsDays
        });
    }

    filter = () => {
        this.componentDidMount();
    }

    handleDayChange1 = (day, modifiers, picker) => {
        var dateArray = picker.getInput().value.split('-');
        this.setState({
            displayDate1: picker.getInput().value,
            date1: dateArray,
            disabled: false
        });
    }

    handleDayChange2 = (day, modifiers, picker) => {

        var dateArray = picker.getInput().value.split('-');
        this.setState({
            date2: dateArray,
            displayDate2: picker.getInput().value
        });
    }

    handleOnChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        });

    }

    handleSearchBar = async (e) => {
        e.preventDefault();
        this.componentDidMount();
    }

    handleCategory = async (e) => {
        e.preventDefault();
        let name = typeof e.target.name === "undefined" ? e.target.getAttribute("name") : e.target.name;
        name = e.target.name === "Wszystkie" ? null : name;

        await this.setState({ category: name });
        this.refreshEvents();
    }

    resetButton = async () => {

        window.location.reload();
        this.refreshEvents();
    }

    refreshEvents = async() => {
        if (this.state.category === "Wszystkie") {
            await this.setState({ category: null });
        }
        let dates;
        if (this.state.date1 !== null) {
            dates = [this.state.date1];

            if (this.state.date2 !== null) dates = dates.concat([this.state.date2]);
            console.log(dates);
        }

        const days = await getAllCalendar(this.state.query, dates, this.state.category === "Wszystkie" ? null : this.state.category);
        const JsDays = days.map((item, key) => {
            return new Date(Date.parse(item));
        })


        if (typeof days !== "undefined" && days.length > 0) {

            const items = JsDays.map((item, index) => {
                console.log(item.latitude)
                if (item.longitude !== null && item.latitude !== null) {
                    return <Marker
                        key={index}
                        position={[item.latitude, item.longitude]}
                    >
                        <Popup>
                            {item.name} <br />
                            {item.address} <br />
                            <Link className="btn btn-primary" to={`/wydarzenie/szczegoly/${item.urlSlug}`}>Przejdź do wydarzenia</Link>
                        </Popup>
                    </Marker>
                }

            }
            );
            console.log(items);
            this.setState({ selectedDays: JsDays });
        }
        else {
            this.setState({ selectedDays: [] });

        }
    }


    render() {
        const MONTHS = [
            'Styczeń',
            'Luty',
            'Marzec',
            'Kwiecień',
            'Maj',
            'Czerwiec',
            'Lipiec',
            'Sierpień',
            'Wrzesień',
            'Pazdziernik',
            'Listopad',
            'Grudzień',
        ];
        const WEEKDAYS_LONG = [
            'Niedziela',
            'Poniedziałek',
            'Wtorek',
            'Sroda',
            'Czwartek',
            'Piatek',
            'Sobota'
        ];
        const WEEKDAYS_SHORT = ['Ndz', 'Pon', 'Wt', 'Sr', 'Czw', 'Pt', 'Sb'];
        return (
            <div className="container">

                <div className="card">
                    <div className="card-header">

                         <React.Fragment>
                    <h1 className="text-center">Kalendarz</h1>
                    <hr />
                    <Modal show={this.state.showModal} onHide={this.closeModal}>
                        <Modal.Header >
                            <Modal.Title >Wydarzenia dnia: {this.state.modalDate}</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            {this.state.modalEvents.map((element, index) => (
                                <div className=" my-4" key={index}>
                                    <Link className="btn btn-sm btn-info mx-2" to={`/wydarzenie/szczegoly/${element.eventUrl}`}>Przejdź</Link>
                                    <b>{element.eventName}</b>

                                    <br />
                                </div>

                            ))}
                        </Modal.Body>
                        <Modal.Footer>
                            <button className="btn btn-secondary" onClick={this.closeModal}>
                                Zamknij
                             </button>
                        </Modal.Footer>

                    </Modal>
                            <div className="text-center">
                        <DayPicker
                                containerProps={{ style: { fontSize: "34px", backgroundColor: "#efffed", borderRadius: "30px" } }}
                            months={MONTHS}
                            weekdaysLong={WEEKDAYS_LONG}
                            weekdaysShort={WEEKDAYS_SHORT}
                            onDayClick={this.onDayClick}
                            format='MM/dd/yyyy'
                            locale="pl"
                            firstDayOfWeek={1}
                            className=" mx-5 mb-5"
                            selectedDays={this.state.selectedDays}
                        />
                        </div>
                        </React.Fragment>

                        <h1>
                            Przefiltruj wszystkie wydarzenia!
                        </h1>

                        <div className="row">
                            <div className="col-md-6">
                                <SearchWidget query={this.state.query} handleSearch={this.handleSearchBar} handleOnChange={this.handleOnChange} allMap={true} />
                            </div>
                            <div className="col-md-6">

                                <div className="card my-4">
                                    <h5 className="card-header" style={{ backgroundColor: "#efffed" }}>Data wydarzenia</h5>
                                    <div className="card-body">
                                        <div className="col-md-6 col-12 nopadding">
                                            <span className="mr-3">OD</span>
                                            <DayPickerInput
                                                className="col-md-4 "
                                                ref="dayPicker"
                                                value={this.state.displayDate1}
                                                month={this.state.displayDate1}
                                                onDayChange={this.handleDayChange1}
                                                placeholder="Kliknij aby wybrać date"
                                                dayPickerProps={{
                                                    months: MONTHS,
                                                    weekdaysLong: WEEKDAYS_LONG,
                                                    weekdaysShort: WEEKDAYS_SHORT,
                                                    showOutsideDays: true,
                                                    fromMonth: new Date(),
                                                    disabledDays: [{
                                                        before: new Date()
                                                    }
                                                    ],
                                                    locale: "pl",
                                                    firstDayOfWeek: 1

                                                }}
                                            />
                                        </div>

                                        <div className="col-md-6 col-12 nopadding">
                                            <span className=" mr-3">DO</span>
                                            <DayPickerInput
                                                className="col-md-4 "
                                                ref="dayPicker1"
                                                value={this.state.displayDate2}
                                                month={this.state.displayDate2}
                                                onDayChange={this.handleDayChange2}
                                                inputProps={{
                                                    disabled: this.state.disabled
                                                }}
                                                placeholder="Puste jeżeli jeden dzień"
                                                dayPickerProps={{
                                                    months: MONTHS,
                                                    weekdaysLong: WEEKDAYS_LONG,
                                                    weekdaysShort: WEEKDAYS_SHORT,
                                                    showOutsideDays: true,
                                                    fromMonth: new Date(),
                                                    disabledDays: [{
                                                        before: new Date()
                                                    }
                                                    ],
                                                    locale: "pl",
                                                    firstDayOfWeek: 1

                                                }}
                                            />
                                        </div>
                                    </div>


                                </div>

                            </div>

                        </div>
                        <CategoriesWidget category={this.state.category} handleSearch={this.handleCategory} handleOnChange={this.handleOnChange} />
                        <div className="text-center">
                            <button className="btn btn-primary mb-3 mr-5" onClick={this.refreshEvents}>Filtruj </button>
                            <button className="btn btn-danger mb-3" onClick={this.resetButton}>Resetuj wyszukiwanie </button>
                        </div>
                    </div>

                </div>
            </div>

        )
    }
}
export default AllCalendar