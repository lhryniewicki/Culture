import React from 'react';
import DayPickerInput from 'react-day-picker/DayPickerInput';
import TimePicker from 'rc-time-picker';
import EventPost from '../EventPost/EventPost';
import 'react-day-picker/lib/style.css';
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
            file:"",
            category: "",
            showPreview: false,
            time:"",
            date: {
                day: "",
                month: "",
                year:""
            }
        };
        this.onCheckBoxClick = this.onCheckBoxClick.bind(this);
        this.handleDayChange = this.handleDayChange.bind(this);
        this.handleTimeChange = this.handleTimeChange.bind(this);
        this.handleFilePick = this.handleFilePick.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.sumbitForm = this.sumbitForm.bind(this);
        this.previewForm = this.previewForm.bind(this);

    }
    componentDidMount() {
        this.refs.dayPicker.input.readOnly = true;
        this.refs.dayPicker.input.setAttribute("class", "form-control");
        this.refs.dayPicker.input.setAttribute("class", "form-control");
        this.refs.dayPicker.input.style.backgroundColor = "white";
        var timePicker = document.getElementsByClassName("rc-time-picker-input");
        timePicker[0].readonly = true;
        timePicker[0].setAttribute("class", "form-control");
    }
    handleDayChange(day, modifiers, picker) {
        var dateArray = picker.getInput().value.split('-');
        this.setState({
            date: {
                year: dateArray[0],
                month: dateArray[1],
                day: dateArray[2]
            }
        });
    }
    handleTimeChange(newTime) {
        this.setState({ time: newTime.format('HH:mm') });
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

    }
    async handleFilePick(event) {
        await this.setState({
            file: event.target.files[0]
        });
    }
    sumbitForm() {

    }
    previewForm() {
        return <EventPost />;
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
                <form onSubmit={this.submitForm}>
                <div className="row">
                    <div className="card my-4 col-md-3">
                        <h5 className="card-header">Kategorie</h5>
                        <div className="card-body">
                            <div className="row">
                                <div className="col-md-7">
                                    <div className="custom-control custom-checkbox" onClick={this.onCheckBoxClick} >
                                        <div className="form-check-inline"  >
                                            <label htmlFor="check1">
                                                    <input type="checkbox" className="form-check-input" id="check1" value="Kat1" />Kat1
                                        </label>
                                        </div>
                                        <div className="form-check-inline">
                                            <label htmlFor="check2">
                                                    <input type="checkbox" className="form-check-input" id="check2" value="Kat2" />Kat2
                                        </label>
                                        </div>
                                        <div className="form-check-inline">
                                            <label htmlFor="check3">
                                                    <input type="checkbox" className="form-check-input" id="check3" value="Kat3" />Kat3
                                        </label>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div className="card my-4 mx-4 col-md-8"  >
                        <h5 className="card-header">Informacje o wydarzeniu</h5>
                        
                            <div className="mt-4">
                                <input type="text"
                                    name={this.state.eventName}
                                    onChange={this.handleInputChange}
                                    className="form-control"
                                    placeholder="Nazwa wydarzenia"
                                    required
                                />
                            </div>
                            <div className="mt-4">
                                <textarea type="text"
                                    className="form-control"
                                    name={this.state.eventDescription}
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
                                weekdaysLong: WEEKDAYS_LONG ,
                                weekdaysShort: WEEKDAYS_SHORT, 
                                    showOutsideDays: true,
                                    fromMonth: new Date(),
                                disabledDays: [{
                                     before: new Date()
                                    }
                                    ],
                                locale:"pl",
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
                                        className="form-control "
                                        placeholder="Wybierz zdjęcie"
                                        value={this.state.file.name}

                                   />
                                </div>
                            </div>
                            <div className="row "> 
                                <div className=" col-md-5 mb-4">
                                    <input type="text"
                                        name={this.state.eventCity}
                                        onChange={this.handleInputChange}
                                        className="form-control" placeholder="Miasto"
                                        required />
                                </div>
                                <div className="col-md-4 mb-4">
                                    <input type="text"
                                        name={this.state.eventStreet}
                                        onChange={this.handleInputChange}
                                        className="form-control" placeholder="Ulica numer/lokal"
                                        required />
                                </div>
                                <div className=" col-md-3 mb-4">
                                    <input type="number"
                                        name={this.state.eventPrice}
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
                                    <button type="submit" className="btn btn-primary">Wyślij</button>
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

        );
    }
}
export default EventForm;



