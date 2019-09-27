import React from 'react';
import '../../components/Widgets/CategoriesWidget.css';


class CategoriesWidget extends React.Component {

    render() {
        return (
            <div className="card my-4">
                <h5 className="card-header">Kategorie</h5>
                <div className="card-body">
                    <div className="row">
                        
                            <div className="col-lg-6 ">
                                <ul className="list-unstyled mb-0">
                                <li name="Muzyka" onClick={this.props.handleSearch} >
                                    <button className="myButton" name="Muzyka" type="submit">
                                        Muzyka
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Turystyka" >
                                    <button className="myButton" name="Turystyka" type="submit">
                                        Turystyka
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Styl Życia">
                                    <button className="myButton" onClick={this.props.handleSearch} name="Styl Życia" type="submit">
                                       Styl Życia
                                     </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Regionalia">
                                    <button className="myButton" onClick={this.props.handleSearch} name="Regionalia" type="submit">
                                       Regionalia
                                     </button>
                                </li>
                                <li onClick={this.props.handleSearch} name="Wszystkie">
                                    <button className="myButton" onClick={this.props.handleSearch} name="Wszystkie" type="submit">
                                        Wszystkie
                                    </button>
                                </li>
                                </ul>
                            </div>
                            <div className="col-lg-6">
                                <ul className="list-unstyled mb-1">
                                <li onClick={this.props.handleSearch} name="Nauka">
                                    <button className="myButton" onClick={this.props.handleSearch} name="Nauka" type="submit">
                                       Nauka
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Sport">
                                    <button className="myButton" onClick={this.props.handleSearch} name="Sport" type="submit">
                                        Sport
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Dom">
                                    <button className="myButton" onClick={this.props.handleSearch} name="Dom" type="submit">
                                       Dom
                                    </button>
                                    </li>
                                <li onClick={this.props.handleSearch} name="Sztuka">
                                    <button className="myButton" onClick={this.props.handleSearch} name="Sztuka" type="submit">
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



