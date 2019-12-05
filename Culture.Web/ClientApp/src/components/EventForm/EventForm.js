import React from 'react';
import DayPickerInput from 'react-day-picker/DayPickerInput';
import TimePicker from 'rc-time-picker';
import EventPost from '../EventPost/EventPost';
import { createEvent } from '../../api/EventApi';
import { userIsAuthenticated } from '../../utils/JwtUtils';
import { Redirect } from 'react-router';
import 'react-day-picker/lib/style.css';
import '../EventForm/EventForm.css'
import 'rc-time-picker/assets/index.css';



class EventForm extends React.Component {


    constructor(props) {
        super(props);
        this.state = {
            eventName: "",
            eventDescription: "",
            eventPrice: 0,
            eventCity: "",
            eventStreet: "",
            file: {
                name: "Wybierz plik"
            },
            eventCategory: "",
            showPreview: false,
            time:"",
            date: [],
            errors: [],
            positiveMessage:""
        };
        this.onCheckBoxClick = this.onCheckBoxClick.bind(this);
        this.handleDayChange = this.handleDayChange.bind(this);
        this.handleTimeChange = this.handleTimeChange.bind(this);
        this.handleFilePick = this.handleFilePick.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.submitForm = this.submitForm.bind(this);
        this.previewForm = this.previewForm.bind(this);

    }
    componentDidMount() {
        if (userIsAuthenticated()) {

            this.refs.dayPicker.input.readOnly = true;
            this.refs.dayPicker.input.setAttribute("class", "form-control");
            this.refs.dayPicker.input.setAttribute("class", "form-control");
            this.refs.dayPicker.input.style.backgroundColor = "white";
            var timePicker = document.getElementsByClassName("rc-time-picker-input");
            timePicker[0].readonly = true;
            timePicker[0].setAttribute("class", "form-control");
        }
    }

    handleDayChange(day, modifiers, picker) {

        var dateArray= picker.getInput().value.split('-');
         this.setState({
             date: dateArray
        });
    }
    handleTimeChange(newTime) {
        if (newTime !== null && typeof newTime !== "undefined") {
            this.setState({ time: newTime.format('HH:mm') });
        }
    }
    handleInputChange(evt) {
        this.setState({ [evt.target.name]: evt.target.value });
    }
    onCheckBoxClick(e) {
        var boxes = document.getElementsByClassName("form-check-input");
        Array.from(boxes).forEach((item) => {
            item.checked = false;
        });
        e.target.checked = true;
        this.setState({ [e.target.name]: e.target.value });
    }
     handleFilePick(event) {
        if (event.target.files[0] === undefined) {
            let file = { name: 'Wybierz plik' }
            this.setState({ file: file })
        }
        else {
             this.setState({
                file: event.target.files[0]
            });
        }
    }
    async submitForm(e) {
        e.preventDefault();
        await this.setState({ errors: [] });
        if (this.state.time === "") {
            await this.setState({ errors: ["Wybierz czas wydarzenia"].concat(this.state.errors) })
            console.log(this.state.date )
        }
        if (this.state.date.length === 0) {
           
            await this.setState({ errors: ["Wybierz date wydarzenia"].concat(this.state.errors) })

        }
        if (this.state.eventCategory === "") {
            await this.setState({ errors: ["Wybierz kategorie wydarzenia"].concat(this.state.errors) })

        }
        if (this.state.errors.length ===0){
            await createEvent(this.state)
                .catch(e => { return e})
                .then(e => {
                    e !== "success" ?
                        this.setState({ errors: [e].concat(this.state.errors) })
                        :
                        this.setState({
                            positiveMessage: "Pomyślnie stworzono wydarzenie",
                            eventCategory:""
                        });
                }

                );
        }
      
    }
    previewForm() {
        return <EventPost
            isPreview={true}
            eventName={this.state.eventName}
            eventDescription={this.state.eventDescription}
            eventCity={this.state.eventCity}
            eventPrice={this.state.eventPrice}
            eventStreet={this.state.eventStreet}
            picture={this.state.file}
            category={this.state.category}
            date={this.state.date}
            time={this.state.time}
            canloadMore={false}
        />;
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
                {this.state.positiveMessage !== "" ?
                <div className="alert alert-success" >
                    {this.state.positiveMessage}
                </div>

                :
                null
            }
                {this.state.errors.length > 0 ? this.state.errors.map((item) => (
                    <div className="row">
                        <div className="alert alert-danger col-md-offset-3 col-md-8" role="alert">
                            {item}
                        </div>
                    </div>
                )
                ) :
                    null
                }
                {userIsAuthenticated()
                    ?
                    <div>

                        <form onSubmit={this.submitForm}>
                            <div className="row">
                                <div className="card my-4 col-md-3">
                                    <h5 className="card-header" style={{ backgroundColor: "#efffed" }}>Kategorie</h5>
                                    <div className="card-body">
                                        <div className="row">
                                            <div className="col-md-9">
                                                <div className="custom-control custom-checkbox" onClick={this.onCheckBoxClick} >
                                                    <div className="form-check-inline"  >
                                                        <label htmlFor="check1">
                                                            <input type="checkbox"  name="eventCategory" className="form-check-input" id="check1" value="Nauka" />Nauka
                                        </label>
                                                    </div>
                                                    <div className="form-check-inline col">
                                                        <label htmlFor="check2">
                                                            <input type="checkbox" name="eventCategory" className="form-check-input" id="check2" value="Sport" />Sport
                                        </label>
                                                    </div>
                                                    <div className="form-check-inline">
                                                        <label htmlFor="check3">
                                                            <input type="checkbox" name="eventCategory" className="form-check-input" id="check3" value="Dom" />Dom
                                        </label>
                                                    </div>
                                                    <div className="form-check-inline"  >
                                                        <label htmlFor="check4">
                                                            <input type="checkbox" name="eventCategory" className="form-check-input" id="check4" value="Sztuka" />Sztuka
                                        </label>
                                                    </div>
                                                    <div className="form-check-inline">
                                                        <label htmlFor="check5">
                                                            <input type="checkbox" name="eventCategory" className="form-check-input" id="check5" value="Muzyka" />Muzyka
                                        </label>
                                                    </div>
                                                    <div className="form-check-inline">
                                                        <label htmlFor="check6">
                                                            <input type="checkbox" name="eventCategory" className="form-check-input" id="check6" value="Turystyka" />Turystyka
                                        </label>
                                                    </div>
                                                    <div className="form-check-inline">
                                                        <label htmlFor="check7">
                                                            <input type="checkbox" name="eventCategory" className="form-check-input" id="check7" value="Styl Życia" />Styl Życia
                                        </label>
                                                    </div>
                                                    <div className="form-check-inline">
                                                        <label htmlFor="check8">
                                                            <input type="checkbox" name="eventCategory" className="form-check-input" id="check8" value="Regionalia" />Regionalia
                                        </label>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div className="card my-4 mx-4 col-md-8"  >
                                    <h5 className="card-header" style={{ backgroundColor: "#efffed" }}>Informacje o wydarzeniu</h5>

                                    <div className="mt-4">
                                        <input type="text"
                                            name="eventName"
                                            value={this.state.eventName}
                                            onChange={this.handleInputChange}
                                            className="form-control"
                                            placeholder="Nazwa wydarzenia"
                                            required
                                        />
                                    </div>
                                    <div className="mt-4">
                                        <textarea type="text"
                                            className="form-control showSpace"
                                            name="eventDescription"
                                            style={{ whiteSpace: "pre-Wrap" }}
                                            value={this.state.eventDescription}
                                            onChange={this.handleInputChange}
                                            placeholder="Opis wydarzenia"
                                            required
                                            rows="6"
                                        />
                                    </div>
                                    <div className="row mx-1 my-4 " >
                                        <DayPickerInput
                                            className="col-md-4 "
                                            ref="dayPicker"
                                            onDayChange={this.handleDayChange}
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
                                        <div className="col-md-4 my-4 my-md-0">
                                            <TimePicker
                                                onChange={this.handleTimeChange}
                                                placeholder="Wybierz godzinę"
                                                showSecond={false}
                                                minuteStep={5}
                                            />

                                        </div>
                                        <div className="col-md-4">
                                            <input style={{ display: "none" }}
                                                type="file"
                                                onChange={this.handleFilePick}
                                                ref={fileInput => this.fileInput = fileInput}
                                            />
                                            <input

                                                type="button"
                                                onClick={() => this.fileInput.click()}
                                                onChange={() => this.fileInput.click()}
                                                className="form-control "
                                                value={this.state.file.name}

                                            />
                                        </div>
                                    </div>
                                    <div className="row ">
                                        <div className=" col-md-5 mb-4">
                                            <input type="text"
                                                name="eventCity"
                                                value={this.state.eventCity}
                                                onChange={this.handleInputChange}
                                                className="form-control" placeholder="Miasto"
                                                required />
                                        </div>
                                        <div className="col-md-4 mb-4">
                                            <input type="text"
                                                name="eventStreet"
                                                value={this.state.eventStreet}
                                                onChange={this.handleInputChange}
                                                className="form-control" placeholder="Ulica numer/lokal"
                                                required />
                                        </div>
                                        <div className=" col-md-3 mb-4">
                                            <input type="number"
                                                min="0"
                                                name="eventPrice"
                                                value={this.state.eventPrice}
                                                onChange={this.handleInputChange}
                                                className="form-control"
                                                placeholder="Cena biletu"
                                                required />
                                        </div>
                                    </div>
                                    <div className="row mb-4">
                                        <div className="col-md-12 text-center">
                                            <button type="button"
                                                onClick={() => { this.setState({ showPreview: !this.state.showPreview }); }}
                                                className="btn btn-success mr-1">Podgląd</button>
                                            <button className="btn btn-primary">Wyślij</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                        <div className="col-md-8 offset-md-2">
                            {this.state.showPreview
                                ?
                                this.previewForm()
                                :
                                null
                            }
                        </div>
                    </div>
                    :
               <Redirect to={`/konto/login`} />
                        }
            </div>

        );
    }
}
export default EventForm;



