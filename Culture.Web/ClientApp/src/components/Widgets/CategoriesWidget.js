import React from 'react';
import '../../components/Widgets/CategoriesWidget.css';


class CategoriesWidget extends React.Component {

    render() {
        return (
            <div className="card my-4">
                <h5 className="card-header" style={{ backgroundColor: "#efffed" }}>Kategorie</h5>
                <div className="card-body">
                    <div className="row">
                        
                            <div className="col-lg-6 ">
                                <ul className="list-unstyled mb-0">
                                <li name="Muzyka" onClick={this.props.handleSearch} >
                                    <button className="myButton" name="Muzyka" style={this.props.category === "Muzyka" ? { backgroundColor:"#efffed"}: null} type="submit">
                                        Muzyka
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Turystyka" >
                                    <button className="myButton" name="Turystyka" style={this.props.category === "Turystyka" ? { backgroundColor: "#efffed" } : null} type="submit">
                                        Turystyka
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Styl Życia">
                                    <button className="myButton" onClick={this.props.handleSearch} style={this.props.category === "Styl Życia" ? { backgroundColor: "#efffed" } : null} name="Styl Życia" type="submit">
                                       Styl Życia
                                     </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Regionalia">
                                    <button className="myButton" onClick={this.props.handleSearch} style={this.props.category === "Regionalia" ? { backgroundColor: "#efffed" } : null} name="Regionalia" type="submit">
                                       Regionalia
                                     </button>
                                </li>
                                <li onClick={this.props.handleSearch} name="Wszystkie">
                                    <button className="myButton" onClick={this.props.handleSearch} style={this.props.category === "Wszystkie" ? { backgroundColor: "#efffed" } : null} name="Wszystkie" type="submit">
                                        Wszystkie
                                    </button>
                                </li>
                                </ul>
                            </div>
                            <div className="col-lg-6">
                                <ul className="list-unstyled mb-1">
                                <li onClick={this.props.handleSearch} name="Nauka">
                                    <button className="myButton" onClick={this.props.handleSearch} style={this.props.category === "Nauka" ? { backgroundColor: "#efffed" } : null} name="Nauka" type="submit">
                                       Nauka
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Sport">
                                    <button className="myButton" onClick={this.props.handleSearch} style={this.props.category === "Sport" ? { backgroundColor: "#efffed" } : null} name="Sport" type="submit">
                                        Sport
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Dom">
                                    <button className="myButton" onClick={this.props.handleSearch} style={this.props.category === "Dom" ? { backgroundColor: "#efffed" } : null} name="Dom" type="submit">
                                       Dom
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Sztuka">
                                    <button className="myButton" onClick={this.props.handleSearch} style={this.props.category === "Sztuka" ? { backgroundColor: "#efffed" } : null} name="Sztuka" type="submit">
                                      Sztuka
                                    </button>
                                    </li>
                                </ul>
                        </div>
                       
                    </div>
                </div>
            </div>
        );
    }
}
export default CategoriesWidget;



