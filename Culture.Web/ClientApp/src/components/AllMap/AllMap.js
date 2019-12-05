import React from 'react';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import { getAllMap } from '../../api/EventApi';
import '../AllMap/AllMap.css';
import { Link } from 'react-router-dom';
import { userIsAuthenticated } from '../../utils/JwtUtils';
import { Redirect } from 'react-router';
import  CategoriesWidget from '../Widgets/CategoriesWidget';
import SearchWidget from '../Widgets/SearchWidget';
import DayPickerInput from 'react-day-picker/DayPickerInput';

class AllMap extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            Markers: [],
            position: [53.1324886, 23.1688403],
            zoom: 11,
            query: null,
            date1: null,
            date2: null,
            displayDate1:null,
            displayDate2:null,
            category: null,
            disabled:true
        }
    }

    componentDidMount() {
        this.refs.dayPicker.input.readOnly = true;
        this.refs.dayPicker.input.setAttribute("class", "form-control");
        this.refs.dayPicker.input.setAttribute("class", "form-control");
        this.refs.dayPicker.input.style.backgroundColor = "white";
        this.refs.dayPicker1.input.readOnly = true;
        this.refs.dayPicker1.input.setAttribute("class", "form-control");
        this.refs.dayPicker1.input.setAttribute("class", "form-control");
        this.refs.dayPicker1.input.style.backgroundColor = "white";
        this.loadMarkers();
    }

    filter = () => {
        this.componentDidMount();
    }

    handleDayChange1 = (day, modifiers, picker)=> {
        var dateArray = picker.getInput().value.split('-');
        this.setState({
            displayDate1: picker.getInput().value,
            date1: dateArray,
            disabled:false
        });
    }

    handleDayChange2=(day, modifiers, picker)=> {

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

    handleCategory = async(e) => {
        e.preventDefault();
        let name = typeof e.target.name === "undefined" ? e.target.getAttribute("name") : e.target.name;
        name = e.target.name === "Wszystkie" ? null : name;

        await this.setState({ category: name });

        this.loadMarkers();
    }

    resetButton = async () => {

        window.location.reload();
        this.filter();
    }

    async loadMarkers() {
        if (this.state.category === "Wszystkie") {
            await this.setState({ category: null });
        } 
        let dates;
        if (this.state.date1 !== null) {
            dates =[this.state.date1];

            if (this.state.date2 !== null) dates = dates.concat([this.state.date2]);
            console.log(dates);
        }

        const data = await getAllMap(this.state.query, dates, this.state.category === "Wszystkie" ? null : this.state.category);
        
        if (typeof data !== "undefined" && data.length > 0) {

            const items = data.map((item, index) => {
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
            this.setState({ Markers: items });
        }
        else {
            this.setState({ Markers: [] });

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

                <div className = "card">
                    <div className="card-header">

                        <Map style={{ height: "800px" }} center={this.state.position} zoom={this.state.zoom}>
                            <TileLayer
                                attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                            />
                            {this.state.Markers.map((item) => {
                                return item;
                            })}
                        </Map>

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
                            <button className="btn btn-primary mb-3 mr-5" onClick={this.filter}>Filtruj </button>
                            <button className="btn btn-danger mb-3" onClick={this.resetButton}>Resetuj wyszukiwanie </button>
                        </div>
                    </div>
                  
                </div>
            </div>

        )
    }
}
export default AllMap